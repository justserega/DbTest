using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockAppCore.Models;

namespace StockAppCore.Services
{
    public class RemainsService
    {
        public static List<RemainItem> GetRemainFor(Storage storage, DateTime time)
        {
            var remains = new List<RemainItem>();
            using (var db = new MyContext())
            {
                var incomes = db.MoveDocuments
                    .Include(x => x.Items)
                    .Where(x => x.DestStorageId == storage.Id && x.Time < time)
                    .Where(x => x.IsDeleted == false)
                    .SelectMany(x => x.Items).ToList();

                var outcomes = db.MoveDocuments
                    .Include(x => x.Items)
                    .Where(x => x.SourceStorageId == storage.Id && x.Time < time)
                    .Where(x => x.IsDeleted == false)
                    .SelectMany(x => x.Items)
                    .ToList();

                AddToRemainList(remains, incomes);
                AddToRemainList(remains, outcomes, -1);
            }

            return remains;
        }       

        private static void AddToRemainList(List<RemainItem> remains, List<MoveDocumentItem> items, int multi = 1)
        {
            foreach(var item in items)
            {
                var remain = remains.FirstOrDefault(x => x.GoodId == item.GoodId);
                if (remain == null)
                {
                    remain = new RemainItem { GoodId = item.GoodId, Count = 0 };
                    remains.Add(remain);
                }

                remain.Count += item.Count * multi;
            }
        }
    }
}