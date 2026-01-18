using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeShare.ViewModels.Content
{
    public class KnowledgeBaseVm
    {
            public int Id { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string Title { get; set; }
            public string SeoAlias { get; set; }
            public string Description { get; set; }
            public string Environment { get; set; }
            public string Problem { get; set; }
            public string? StepToReproduce { get; set; }
            public string ErrorMessage { get; set; }

            public string Workaround { get; set; }

            public string? Note { get; set; }

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

