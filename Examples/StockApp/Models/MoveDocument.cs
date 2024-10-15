using System;
using System.Collections.Generic;

namespace StockAppCore.Models
{
    public class MoveDocument
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Time { get; set; }
        public bool IsDeleted { get; set; }

        public int SourceStorageId { get; set; }
        public int DestStorageId { get; set; }

        // Related models
        public ICollection<MoveDocumentItem> Items { get; set; }
    }

    public class MoveDocumentItem
    {
        public int Id { get; set; }
        public int MoveDocumentId { get; set; }
        public int GoodId { get; set; }
        public decimal Count { get; set; }
        public bool IsDeleted { get; set; }

        // Related models
        public Good Good { get; set; }
    }
}
