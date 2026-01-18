using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeShare.ViewModels.Content
{
    public class CommentVm
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public int KnowledgeBaseId { get; set; }
        public string OwnerUserId { get; set; }
        public DateTime CreateDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? LastModifiedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
