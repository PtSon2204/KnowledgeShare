using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeShare.ViewModels.Content
{
    public class ReportVm
    {
        public int Id { get; set; }

        public int? KnowledgeBaseId { get; set; }

        public string Content { get; set; }
        public string ReportUserId { get; set; }
        public bool IsProcessed { get; set; }
      
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
