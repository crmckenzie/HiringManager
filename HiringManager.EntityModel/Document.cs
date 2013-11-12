using System.ComponentModel.DataAnnotations;

namespace HiringManager.EntityModel
{
    public class Document
    {
        public int? DocumentId { get; set; }

        [StringLength(1000)]
        public string FileName { get; set; }
        public string StorageId { get; set; }
    }
}