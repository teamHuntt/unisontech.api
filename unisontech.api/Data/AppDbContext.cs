using Microsoft.EntityFrameworkCore;
using unisontech.api.Models;

namespace unisontech.api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Member>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Loan>()
            .Property(l => l.Id)
            .ValueGeneratedOnAdd();

        // Relationships
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Member)
            .WithMany(m => m.Loans)
            .HasForeignKey(l => l.MemberId);

        modelBuilder.Entity<Book>().HasData(
                // seeding the database with some initial data for the Book entity
                new Book
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    ISBN = "978-0132350884",
                    Genre = "Technology",
                    Quantity = 5
                },
                new Book
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt",
                    ISBN = "978-0201616224",
                    Genre = "Technology",
                    Quantity = 3
                },


                new Book
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "The Three Mistakes of My Life",
                    Author = "Chetan Bhagat",
                    ISBN = "978-8129115300",
                    Genre = "Fiction",
                    Quantity = 4
                },
                new Book
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    ISBN = "978-0061935466",
                    Genre = "Classic Fiction",
                    Quantity = 5
                },
                new Book
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Title = "The Alchemist",
                    Author = "Paulo Coelho",
                    ISBN = "978-0062315007",
                    Genre = "Classic Fiction",
                    Quantity = 6
                }
            );
        modelBuilder.Entity<Member>().HasData(
              new Member
              {
                  Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                  Name = "John Doe",
                  Email = "john.doe@email.com",
                  Phone = "0123456789",
                  MembershipDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                  CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                  IsActive = true,
                  IsEmailVerified = true
              },
              new Member
              {
                  Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                  Name = "Jane Smith",
                  Email = "jane.smith@email.com",
                  Phone = "0182549316",
                  MembershipDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                  CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                  IsActive = true,
                  IsEmailVerified = true
              }
        );
    }
}