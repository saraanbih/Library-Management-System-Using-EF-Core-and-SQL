using Library_Management_System_Task.Data;
using Library_Management_System_Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_Task.Services
{
    public class BorrowerService
    {
        private readonly AppDbContext _context;
        public BorrowerService(AppDbContext context) => _context = context;
        public async Task<List<Borrower>> GetAllBorrowersAsync() => await _context.Borrowers.ToListAsync();
        public async Task<Borrower?> GetBorrowerByIdAsync(int id) => await _context.Borrowers.FindAsync(id);
        public async Task AddBorrowerAsync(Borrower borrower)
        {
            _context.Borrowers.Add(borrower);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBorrowerAsync(Borrower borrower)
        {
            _context.Borrowers.Update(borrower);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBorrowerAsync(int id)
        {
            var borrower = await GetBorrowerByIdAsync(id);
            if (borrower != null)
            {
                _context.Borrowers.Remove(borrower);
                await _context.SaveChangesAsync();
            }
        }
    }
}
