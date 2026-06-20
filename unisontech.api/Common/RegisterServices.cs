using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using unisontech.api.Data;
using unisontech.api.Repository;
using unisontech.api.Services;

namespace unisontech.api.Common
{
    public static class RegisterServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=library.db"));
            
            // API Versioning
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true; 
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Repositories
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            // Services
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ILoanService, LoanService>();

            return services;
        }
    }
}
