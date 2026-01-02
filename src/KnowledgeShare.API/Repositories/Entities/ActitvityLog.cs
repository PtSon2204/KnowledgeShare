using System.ComponentModel.DataAnnotations.Schema;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeSpace.BackendServer.Data.Entities;

namespace KnowledgeShare.API.Repositories.Entities
{
    [Table("ActivityLogs")]
    public class ActivityLog : IDateTracking
    {
        [Key]
        public int Id { get; set; } // Khóa chính tự tăng (Primary Key)

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Action { get; set; } // Ví dụ: 'Create', 'Update', 'Delete', 'Login'

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string EntityName { get; set; } // Tên bảng bị tác động (ví dụ: 'KnowledgeBases')

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string EntityId { get; set; } // ID của bản ghi bị tác động

        [Required]
        public DateTime CreateDate { get; set; } // Thời điểm thực hiện hành động

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string UserId { get; set; } // Người thực hiện hành động
        [MaxLength(500)]
        public string Content { get; set; } // Nội dung chi tiết hoặc JSON thay đổi
        public DateTime? LastModifiedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
