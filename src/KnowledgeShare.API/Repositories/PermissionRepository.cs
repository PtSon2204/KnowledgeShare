using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PermissionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<PermissionVm>> GetAllCommandViews()
        {
            var query = from f in _dbContext.Functions
                        join cif in _dbContext.CommandInFunctions on f.Id equals cif.FunctionId
                        join sa in _dbContext.Commands on cif.CommandId equals sa.Id into saGroup
                        from sa in saGroup.DefaultIfEmpty() // Left Join
                        group sa by new { f.Id, f.Name, f.ParentId } into g
                        orderby g.Key.ParentId
                        select new PermissionVm
                        {
                            Id = g.Key.Id,
                            Name = g.Key.Name,
                            ParentId = g.Key.ParentId,
                            HasCreate = g.Any(x => x.Id == "CREATE"),
                            HasUpdate = g.Any(x => x.Id == "UPDATE"),
                            HasDelete = g.Any(x => x.Id == "DELETE"),
                            HasView = g.Any(x => x.Id == "VIEW"),
                            HasApprove = g.Any(x => x.Id == "APPROVE"),
                        };

            return await query.ToListAsync();
        }
    }
}
