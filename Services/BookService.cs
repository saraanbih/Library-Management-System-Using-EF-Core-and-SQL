using Library_Management_System_Task.Data;
using Library_Management_System_Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_Task.Services
{
    public class BookService
    {
        private readonly AppDbContext _context;
        public BookService(AppDbContext context) => _context = context;
        
        public async Task<List<Book>> GetAllBooksAsync() => await _context.Books.Include(b => b.Author).ToListAsync();
        
        public async Task<Book?> GetBookByIdAsync(int id) => await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        
        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
