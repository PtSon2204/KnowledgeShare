using System.ComponentModel.DataAnnotations.Schema;
using KnowledgeShare.API.Repositories.Interface;

namespace KnowledgeShare.API.Repositories.Entities
{
    [Table("Attachments")]
    public class Attachment : IDateTracking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string FileName { get; set; }
        [Required]
        [MaxLength(200)]
        public string FilePath { get; set; }
        [Required]
        [MaxLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string FileType { get; set; }
        [Required]
        public long FileSize { get; set; }
        public int? KnowledgeBaseId { get; set; }
        public int? CommentId { get; set; }
        [Required]
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Type {  get; set; }

        public DateTime CreateDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? LastModifiedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
       
    }
}
