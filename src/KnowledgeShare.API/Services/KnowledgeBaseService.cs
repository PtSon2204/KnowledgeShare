using KnowledgeShare.API.Repositories;
using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.API.ViewModels;
using KnowledgeShare.ViewModels.Content;
using KnowledgeSpace.BackendServer.Data.Entities;

namespace KnowledgeShare.API.Services
{
    public class KnowledgeBaseService : IKnowledgeBaseService
    {
        private readonly IKnowledgeBaseRepository _knowledgeBaseRepository;

        public KnowledgeBaseService(IKnowledgeBaseRepository knowledgeBaseRepository)
        {
            _knowledgeBaseRepository = knowledgeBaseRepository;
        }

        public async Task<CreateKnowledgeBaseRequest> CreateKnowledgeBaseRequestAsync(CreateKnowledgeBaseRequest knowledge)
        {
            var knowledgeBase = new KnowledgeBase
            {
                CategoryId = knowledge.CategoryId,
                Title = knowledge.Title,
                SeoAlias = knowledge.SeoAlias,
                Description = knowledge.Description,
                Environment = knowledge.Environment,
                Problem = knowledge.Problem,
                StepToReproduce = knowledge.StepToReproduce,
                ErrorMessage = knowledge.ErrorMessage,
                Workaround = knowledge.Workaround,
                Note = knowledge.Note,
                Labels = knowledge.Labels
            };

            await _knowledgeBaseRepository.CreateKnowledgeBaseAsync(knowledgeBase);
            return knowledge;
        }

        public async Task<bool> DeleteKnowledgeBaseRequestAsync(int id)
        {
            var result = await _knowledgeBaseRepository.GetKnowledgeBaseByIdAsync(id);

            if (result == null)
            {
                return false;
            }
            return await _knowledgeBaseRepository.DeleteKnowledgeBaseAsync(result);
        }

        public async Task<Pagination<CommentVm>> GetAllCommentPaging(int knowledgeBaseId, string keyword, int pageIndex, int pageSize)
        {
            var result = await _knowledgeBaseRepository.GetAllCommentPaging(knowledgeBaseId , keyword, pageIndex, pageSize);

            return new Pagination<CommentVm>
            {
                Items = result.Items.Select(c => new CommentVm()
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreateDate = c.CreateDate,
                    KnowledgeBaseId = c.KnowledgeBaseId,
                    LastModifiedDate = c.LastModifiedDate,
                    OwnerUserId = c.OwnerUserId,
                }).ToList(),
                TotalRecords = result.TotalRecords
            };
        }

        public async Task<List<KnowledgeBaseQuickVm>> GetAllKnowledgeBaseRequestsAsync()
        {
            var list = await _knowledgeBaseRepository.GetAllKnowledgeBasesAsync();

            return list.Select(x => new KnowledgeBaseQuickVm
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Description = x.Description,
                SeoAlias = x.SeoAlias,
                Title = x.Title,
            }).ToList();
        }

        public async Task<KnowledgeBaseVm> GetKnowledgeBaseRequestByIdAsync(int id)
        {
            var knowledge = await _knowledgeBaseRepository.GetKnowledgeBaseByIdAsync(id);

            return new KnowledgeBaseVm
            {
                Id = knowledge.Id,
                CategoryId = knowledge.CategoryId,
                Title = knowledge.Title,
                SeoAlias = knowledge.SeoAlias,
                Description = knowledge.Description,
                Environment = knowledge.Environment,
                Problem = knowledge.Problem,
                StepToReproduce = knowledge.StepToReproduce,
                ErrorMessage = knowledge.ErrorMessage,
                Workaround = knowledge.Workaround,
                Note = knowledge.Note,
                OwnerUserId = knowledge.OwnerUserId,
                Labels = knowledge.Labels,
                CreateDate = knowledge.CreateDate,
                LastModifiedDate = knowledge.LastModifiedDate,
                NumberOfComments = knowledge.NumberOfComments,
                NumberOfReports = knowledge.NumberOfReports,
                NumberOfVotes = knowledge.NumberOfVotes,
            };
        }

        public async Task<CreateKnowledgeBaseRequest> UpdateKnowledgeBaseRequestAsync(int id, CreateKnowledgeBaseRequest knowledge)
        {
            var knowledgeBase = await _knowledgeBaseRepository.GetKnowledgeBaseByIdAsync(id);

            if (knowledgeBase == null)
            {
                return null;
            }

            knowledgeBase.CategoryId = knowledge.CategoryId;
            knowledgeBase.Title = knowledge.Title;
            knowledgeBase.SeoAlias = knowledge.SeoAlias;
            knowledgeBase.Description = knowledge.Description;
            knowledgeBase.Environment = knowledge.Environment;
            knowledgeBase.Problem = knowledge.Problem;
            knowledgeBase.StepToReproduce = knowledge.StepToReproduce;
            knowledgeBase.ErrorMessage = knowledge.ErrorMessage;
            knowledgeBase.Workaround = knowledge.Workaround;
            knowledgeBase.Note = knowledge.Note;
            knowledgeBase.Labels = knowledge.Labels;

            await _knowledgeBaseRepository.UpdateKnowledgeBaseAsync(knowledgeBase);

            return knowledge;
        }

        public async Task<CommentVm> GetCommentVmByIdAsync( int commentId)
        {
            var comment = await _knowledgeBaseRepository.GetCommentDetailRepo(commentId);

            return new CommentVm
            {
                Id = comment.Id,
                Content = comment.Content,
                CreateDate = comment.CreateDate,
                KnowledgeBaseId = comment.KnowledgeBaseId,
                LastModifiedDate = comment.LastModifiedDate,
                OwnerUserId = comment.OwnerUserId,
            };
        }
        public async Task<CommentCreateRequest> CreateCommentAsync(CommentCreateRequest comment)
        {
            var commentCreate = new Comment
            {
                Content = comment.Content,
                KnowledgeBaseId = comment.KnowledgeBaseId,
                OwnerUserId = "" //Get user from claim
            };

            await _knowledgeBaseRepository.CreateCommentRepo(commentCreate);
            return comment;
        }
        public async Task<CommentCreateRequest> UpdateCommentAsync(int commentId, CommentCreateRequest comment, string currentUserName)
        {
            var commentUp = await _knowledgeBaseRepository.GetCommentDetailRepo( commentId);

            if (commentUp.OwnerUserId != currentUserName)
            {
                throw new UnauthorizedAccessException("Bạn không có quyền sửa comment này");
            }
            commentUp.Content = comment.Content;

            await _knowledgeBaseRepository.UpdateCommentRepo(commentUp);

            return comment;
        }

        public async Task<bool> DeleteCommentAsync( int commentId)
        {
            var comment = await _knowledgeBaseRepository.GetCommentDetailRepo( commentId);

            if (comment == null)
            {
                return false;
            }

            await _knowledgeBaseRepository.DeleteCommentRepo(comment);
            
            return true;
        }
    }
}
