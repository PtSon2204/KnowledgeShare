using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeShare.ViewModels.Content
{
    public class ReportCreateRequest
    {
        public int? KnowledgeBaseId { get; set; }

        public int? CommentId { get; set; }
        public string Content { get; set; }

        public string ReportUserId { get; set; }
    }
}
