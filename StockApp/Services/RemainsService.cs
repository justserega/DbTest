using Microsoft.EntityFrameworkCore;
using StockApp;
using StockAppCore.Models;

namespace StockAppCore.Services
{
    public class RemainsService
    {
        StockDbContext _context;
        public RemainsService(StockDbContext context)
        {
            _context = context;
        }

        public List<RemainItem> GetRemainFor(Storage storage, DateTime time)
        {
            var remains = new List<RemainItem>();
            
            var incomes = _context.MoveDocuments
                .Include(x => x.Items)
                .Where(x => x.DestStorageId == storage.Id && x.Time < time)
                .Where(x => x.IsDeleted == false)
                .SelectMany(x => x.Items).ToList();

            var outcomes = _context.MoveDocuments
                .Include(x => x.Items)
                .Where(x => x.SourceStorageId == storage.Id && x.Time < time)
                .Where(x => x.IsDeleted == false)
                .SelectMany(x => x.Items)
                .ToList();

            AddToRemainList(remains, incomes);
            AddToRemainList(remains, outcomes, -1);

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