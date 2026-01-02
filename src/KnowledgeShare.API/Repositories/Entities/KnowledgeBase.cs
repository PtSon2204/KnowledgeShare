
using System.ComponentModel.DataAnnotations.Schema;
using KnowledgeShare.API.Repositories.Interface;

namespace KnowledgeShare.API.Repositories.Entities
{
    [Table("KnowledgeBases")]
    public class KnowledgeBase : IDateTracking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, Double.PositiveInfinity)]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string SeoAlias { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string Environment { get; set; }
        [MaxLength(500)]
        public string Problem { get; set; }

        public string? StepToReproduce { get; set; }
        [MaxLength(500)]
        public string ErrorMessage { get; set; }

        [MaxLength(500)]
        public string Workaround { get; set; }

        public string? Note { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string OwnerUserId { get; set; } // Khóa ngoại tới bảng Users

        public string Labels { get; set; } // Danh sách tag cách nhau bởi dấu phẩy

        public int? NumberOfComments { get; set; }

        public int? NumberOfVotes { get; set; }

        public int? NumberOfReports { get; set; }

        public string? Type { get; set; }

        public DateTime CreateDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? LastModifiedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
