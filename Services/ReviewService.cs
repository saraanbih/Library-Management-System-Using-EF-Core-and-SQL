using Library_Management_System_Task.Data;
using Library_Management_System_Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_Task.Services
{
    public class ReviewService
    {
        private readonly AppDbContext _context;
        public ReviewService(AppDbContext context) => _context = context;
        
        public async Task<List<Review>> GetAllReviewsAsync() => 
                await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.Borrower)
                .ToListAsync();
        
        public async Task<Review?> GetReviewByIdAsync(int id) =>
                await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.Borrower)
                .FirstOrDefaultAsync(r => r.Id == id);
        
        public async Task AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteReviewAsync(int id)
        {
            var review = await GetReviewByIdAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
