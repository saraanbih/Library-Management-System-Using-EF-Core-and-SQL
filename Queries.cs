
using Library_Management_System_Task.Data;
using Library_Management_System_Task.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library_Management_System_Task
{
    public class Queries
    {
        private readonly AppDbContext _context;
        public Queries(AppDbContext context) => _context = context;

        // 1: Which books are overdue and who borrowed them?
        public async Task<List<(Book, Borrower)>> GetOverdueBooksAsync()
        {
            return await _context.BorrowedBooks
                .Where(bb => bb.ReturnedDate == null && bb.DueDate < DateTime.Now)
                .Include(bb => bb.Book)
                .Include(bb => bb.Borrower)
                .Select(bb => new ValueTuple<Book, Borrower>(bb.Book, bb.Borrower))
                .ToListAsync();
        }

        // 2: What are the top 3 books with the highest average review rating?
        public async Task<List<(Book, double)>> GetTop3BooksByRatingAsync()
        {
            return await _context.Reviews
                .GroupBy(r => r.Book)
                .Select(g => new { Book = g.Key, AvgRating = g.Average(r => r.Rating) })
                .OrderByDescending(x => x.AvgRating)
                .Take(3)
                .Select(x => new ValueTuple<Book, double>(x.Book, (double)x.AvgRating))
                .ToListAsync();
        }

        // 3: Which books belong to a specific genre and were written by a specific author?
        public async Task<List<Book>> GetBooksByGenreAndAuthorAsync(string genre, string authorName)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Where(b => b.Genre == genre && b.Author.Name.Contains(authorName))
                .ToListAsync();
        }

        // 4: Who are the borrowers that never returned books late?
        public async Task<List<Borrower>> GetBorrowersWithNoLateReturnsAsync()
        {
            return await _context.Borrowers
                .Where(b => !_context.BorrowedBooks.Any(bb => bb.BorrowerId == b.Id && bb.ReturnedDate > bb.DueDate))
                .ToListAsync();
        }

        // 5: What are the ratings and reviewer names for a specific book?
        public async Task<List<(string ReviewerName, int Rating)>> GetBookReviewsAsync(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .Include(r => r.Borrower)
                .Select(r => new ValueTuple<string, int>(r.Borrower.Name, (int)r.Rating))
                .ToListAsync();
        }

        // 6: Which books have never been borrowed?
        public async Task<List<Book>> GetBooksNeverBorrowedAsync()
        {
            return await _context.Books
                .Where(b => !_context.BorrowedBooks.Any(bb => bb.BookId == b.Id))
                .ToListAsync();
        }

        // 7: Which books have no reviews at all?
        public async Task<List<Book>> GetBooksWithNoReviewsAsync()
        {
            return await _context.Books
                .Where(b => !_context.Reviews.Any(r => r.BookId == b.Id))
                .ToListAsync();
        }

        // 8: What is the average rating of books per genre?
        public async Task<List<(string Genre, double AvgRating)>> GetAverageRatingPerGenreAsync()
        {
            return await _context.Reviews
                .Include(r => r.Book)
                .GroupBy(r => r.Book.Genre)
                .Select(g => new ValueTuple<string, double>(g.Key, (double)g.Average(r => r.Rating)))
                .ToListAsync();
        }

        // 9: Which borrowers have written more than 3 reviews?
        public async Task<List<Borrower>> GetBorrowersWithMoreThan3ReviewsAsync()
        {
            return await _context.Reviews
                .GroupBy(r => r.Borrower)
                .Where(g => g.Count() > 3)
                .Select(g => g.Key)
                .ToListAsync();
        }

        // 10: What are the most borrowed books in the last year?
        public async Task<List<(Book, int BorrowCount)>> GetMostBorrowedBooksLastYearAsync()
        {
            var lastYear = DateTime.Now.AddYears(-1);
            return await _context.BorrowedBooks
                .Where(bb => bb.BorrowedDate >= lastYear)
                .GroupBy(bb => bb.Book)
                .Select(g => new ValueTuple<Book, int>(g.Key, g.Count()))
                .OrderByDescending(x => x.Item2)
                .ToListAsync();
        }

        // 11: Which author has written the most books?
        public async Task<Author?> GetAuthorWithMostBooksAsync()
        {
            return await _context.Books
                .GroupBy(b => b.Author)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();
        }

        // 12: Which authors have books that were never reviewed?
        public async Task<List<Author>> GetAuthorsWithNoReviewedBooksAsync()
        {
            return await _context.Authors
                .Where(a => !_context.Reviews.Any(r => r.Book.AuthorId == a.Id))
                .ToListAsync();
        }
    }
}
