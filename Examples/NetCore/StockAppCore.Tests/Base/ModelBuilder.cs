using StockAppCore.Models;
using System;
using System.Globalization;

namespace StockAppCore.Tests.Base
{
    public class ModelBuilder
    {
        public MoveDocument CreateDocument(string time, Storage source, Storage dest)
        {
            var document = new MoveDocument
            {
                Number = "#",

                SourceStorageId = source.Id,
                DestStorageId = dest.Id,

                Time = ParseTime(time),
                IsDeleted = false
            };

            using (var db = new MyContext())
            {
                db.MoveDocuments.Add(document);
                db.SaveChanges();
            }

            return document;
        }

        public MoveDocumentItem AddGood(MoveDocument document, Good good, decimal count)
        {
            var item = new MoveDocumentItem
            {
                MoveDocumentId = document.Id,
                GoodId = good.Id,
                Count = count
            };

            using (var db = new MyContext())
            {
                db.MoveDocumentItems.Add(item);
                db.SaveChanges();
            }

            return item;
        }

        private DateTime ParseTime(string str)
        {
            return DateTime.ParseExact(str, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
