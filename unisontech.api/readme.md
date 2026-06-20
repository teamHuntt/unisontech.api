# ?? Library Management System API

A RESTful Web API built with ASP.NET Core 8 for managing books, members, and loans in a library system.

## ??? Tech Stack

- **ASP.NET Core 8 Web API**
- **Entity Framework Core 8**
- **SQLite** — no database installation required, auto-created on first run
- **Repository + Service Pattern**
- **Swagger / OpenAPI**

## ? Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- No database setup needed — SQLite file is auto-generated!

## ?? How to Run

```bash
git clone <your-repo-url>
cd unisontech.api
dotnet run
```

> On first run, `library.db` is automatically created with seeded data:
> - ?? 5 Books
> - ?? 2 Members

Then open Swagger UI at:



## ?? API Endpoints

### ?? Books
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/books | Get all books |
| GET | /api/books/{id} | Get book by ID |
| POST | /api/books | Add new book |
| PUT | /api/books/{id} | Update book |
| DELETE | /api/books/{id} | Delete book |

### ?? Members
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/members | Get all members |
| GET | /api/members/{id} | Get member by ID |
| POST | /api/members | Register new member |
| PUT | /api/members/{id} | Update member |
| DELETE | /api/members/{id} | Delete member |

### ?? Loans
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/loans | Get all loans |
| GET | /api/loans/{id} | Get loan by ID |
| GET | /api/loans/member/{memberId} | Get loans by member |
| GET | /api/loans/book/{bookId} | Get loans by book |
| POST | /api/loans/borrow | Borrow a book |
| PUT | /api/loans/return/{loanId} | Return a book |
| DELETE | /api/loans/{id} | Delete loan record |

## ?? Business Rules

- ISBN must be unique per book
- Member email must be unique
- Book must be in stock to be borrowed
- Member must be active to borrow
- Member cannot borrow the same book twice simultaneously
- Book quantity automatically decreases on borrow and increases on return
- Due date must be set in the future

## ?? Seeded Data

On first run the following data is auto-inserted:

### Books
| Title | Author | Genre |
|-------|--------|-------|
| Clean Code | Robert C. Martin | Technology |
| The Pragmatic Programmer | Andrew Hunt | Technology |
| The Three Mistakes of My Life | Chetan Bhagat | Fiction |
| To Kill a Mockingbird | Harper Lee | Classic Fiction |
| The Alchemist | Paulo Coelho | Classic Fiction |

### Members
| Name | Email |
|------|-------|
| John Doe | john.doe@email.com |
| Jane Smith | jane.smith@email.com |