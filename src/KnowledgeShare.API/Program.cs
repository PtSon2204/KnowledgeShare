
using FluentValidation.AspNetCore;
using FluentValidation;
using KnowledgeShare.API.Repositories;
using KnowledgeShare.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KnowledgeShare.ViewModels.ViewModels.Validator;
using KnowledgeShare.API.Services.Interface;

namespace KnowledgeShare.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Lấy Connection String từ appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Sign in DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            //Seeding Data
            builder.Services.AddScoped<SeedingData>();

            // Register repositories
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IFunctionRepository, FunctionRepository>();
            builder.Services.AddScoped<ICommanRepository, CommandRepository>();
            builder.Services.AddScoped<ICommandsInFunctionRepository, CommandInFunctionRepository>();

            // Register services
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IRegisterService, RegisterService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFunctionService, FunctionService>();
            builder.Services.AddScoped<ICommandService, CommandService>();  
            builder.Services.AddScoped<ICommandInFunctionService, CommandInFunctionService>();

            //Sign in Identity
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                //// Lockout settings. //nhập sai sau 5 lần sẽ khóa account
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;
            });

            // --- Đăng ký FluentValidation ---
            builder.Services.AddFluentValidationAutoValidation(); // Tự động validate khi nhận Request
            builder.Services.AddFluentValidationClientsideAdapters(); // Hỗ trợ validate phía Client (nếu cần)

            // Đăng ký tất cả Validator có trong Assembly (Khuyên dùng)
            // Nó sẽ tự quét và đăng ký RoleVmValidator và các Validator khác cùng project
            builder.Services.AddValidatorsFromAssemblyContaining<RoleVmValidator>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Tạo một Scope để lấy các service cần thiết
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var seeder = services.GetRequiredService<SeedingData>();
                    await seeder.Seed(); // Gọi hàm chạy seeding
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Một lỗi đã xảy ra trong quá trình Seeding dữ liệu.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); 
            app.UseAuthorization(); 


            app.MapControllers();

            app.Run();
        }
    }
}
