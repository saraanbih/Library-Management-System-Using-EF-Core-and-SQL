using Library_Management_System_Task.Data;
using Library_Management_System_Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_Task.Services
{
    public class BorrowedBookService
    {
        private readonly AppDbContext _context;
        public BorrowedBookService(AppDbContext context) => _context = context;
        
        public async Task<List<BorrowedBook>> GetAllBorrowedBooksAsync()
        => await _context.BorrowedBooks
                .Include(bb => bb.Book)
                .Include(bb => bb.Borrower)
                .ToListAsync();
        
        public async Task<BorrowedBook?> GetBorrowedBookByIdAsync(int id) 
        => await _context.BorrowedBooks
                .Include(bb => bb.Book)
                .Include(bb => bb.Borrower)
                .FirstOrDefaultAsync(bb => bb.Id == id);
        
        public async Task AddBorrowedBookAsync(BorrowedBook borrowedBook)
        {
            _context.BorrowedBooks.Add(borrowedBook);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBorrowedBookAsync(BorrowedBook borrowedBook)
        {
            _context.BorrowedBooks.Update(borrowedBook);
            await _context.SaveChangesAsync();
        }
        public async Task ReturnBookAsync(int borrowedBookId)
        {
            var borrowedBook = await _context.BorrowedBooks.FindAsync(borrowedBookId);
            if (borrowedBook != null && borrowedBook.ReturnedDate == null)
            {
                borrowedBook.ReturnedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        public async Task BorrowBookAsync(int bookId, int borrowerId)
        {
            var book = await _context.Books.FindAsync(bookId);
            var borrower = await _context.Borrowers.FindAsync(borrowerId);

            if (book != null && borrower != null)
            {
                var borrowedBook = new BorrowedBook
                {
                    BookId = book.Id,
                    BorrowerId = borrower.Id,
                    BorrowedDate = DateTime.Now
                };
                _context.BorrowedBooks.Add(borrowedBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBorrowedBookAsync(int id)
        {
            var borrowedBook = await GetBorrowedBookByIdAsync(id);
            if (borrowedBook != null)
            {
                _context.BorrowedBooks.Remove(borrowedBook);
                await _context.SaveChangesAsync();
            }
        }
    }
}
