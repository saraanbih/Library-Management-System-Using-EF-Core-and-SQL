using Library_Management_System_Task.Data;
using Library_Management_System_Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_Task.Services
{
    public class AuthorService
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context) => _context = context;
        
        public async Task<List<Author>> GetAllAuthorsAsync() => await _context.Authors.ToListAsync();
        
        public async Task<Author?> GetAuthorByIdAsync(int id) => await _context.Authors.FindAsync(id);
        
        public async Task AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAuthorAsync(int id)
        {
            var author = await GetAuthorByIdAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}
