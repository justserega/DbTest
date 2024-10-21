using StockAppCore.Models;
using static StockApp.Tests.Utils.ParseUtils;

namespace StockAppCore.Tests
{
    public class DocumentBuilder
    {
        public int DocumentId { get; private set; }
        public DocumentBuilder(int documentId) 
        {
            DocumentId = documentId;
        }

        public static DocumentBuilder CreateMoveDocument(string time, Storage source, Storage dest)
        {
            var document = new MoveDocument
            {
                Number = "#",

                SourceStorageId = source.Id,
                DestStorageId = dest.Id,

                Time = ParseTime(time),
                IsDeleted = false
            };

            using (var db = SandBox.GetStockDbContext())
            {
                db.MoveDocuments.Add(document);
                db.SaveChanges();
            }

            return new DocumentBuilder(document.Id);
        }

        public DocumentBuilder AddGood(Good good, decimal count)
        {
            var item = new MoveDocumentItem
            {
                MoveDocumentId = DocumentId,
                GoodId = good.Id,
                Count = count
            };

            using (var db = SandBox.GetStockDbContext())
            {
                db.MoveDocumentItems.Add(item);
                db.SaveChanges();
            }

            return this;
        }
    }


}
