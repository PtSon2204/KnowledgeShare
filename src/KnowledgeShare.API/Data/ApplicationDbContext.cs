using KnowledgeShare.API.Repositories.Entities;
using KnowledgeSpace.BackendServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Cấu hình cho IdentityRole (tối ưu độ dài Id)
            builder.Entity<IdentityRole>().Property(x => x.Id)
                .HasMaxLength(50).IsUnicode(false);

            // Cấu hình cho User (tối ưu độ dài Id)
            builder.Entity<User>().Property(x => x.Id)
                .HasMaxLength(50).IsUnicode(false);

            // Cấu hình Khóa chính hỗn hợp cho LabelInKnowledgeBase
            builder.Entity<LabelInKnowledgeBase>()
                .HasKey(c => new { c.LabelId, c.KnowledgeBaseId });

            // Cấu hình Khóa chính hỗn hợp cho Permission
            builder.Entity<Permission>()
                .HasKey(c => new { c.RoleId, c.FunctionId, c.CommandId });

            // Cấu hình Khóa chính hỗn hợp cho Vote
            builder.Entity<Vote>()
                .HasKey(c => new { c.KnowledgeBaseId, c.UserId });

            // Cấu hình Khóa chính hỗn hợp cho CommandInFunction
            builder.Entity<CommandInFunction>()
                .HasKey(c => new { c.CommandId, c.FunctionId });

            // Cấu hình Sequence cho KnowledgeBase (tự động tăng theo quy luật riêng nếu cần)
            builder.HasSequence("KnowledgeBaseSequence");

            // Đảm bảo các bảng Identity có tên đẹp hơn
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        }

        public DbSet<Command> Commands { get; set; }
        public DbSet<CommandInFunction> CommandInFunctions { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<KnowledgeBase> KnowledgeBases { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<LabelInKnowledgeBase> LabelInKnowledgeBases { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
    }
}
