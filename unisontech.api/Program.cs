using Microsoft.EntityFrameworkCore; //this is required for .Migrate()
using unisontech.api.Common;
using unisontech.api.Data;
using unisontech.api.Middlewares;
using unisontech.api.Repository;
using unisontech.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Repository
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

// Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ILoanService, LoanService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register services using the extension method
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

//Auto-apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); //works now
}

// Global exception handler
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();