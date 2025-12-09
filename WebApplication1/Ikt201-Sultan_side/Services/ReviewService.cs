using Ikt201_Sultan_side.Data;
using Ikt201_Sultan_side.Models;
using Microsoft.EntityFrameworkCore;

namespace Ikt201_Sultan_side.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(ApplicationDbContext context, ILogger<ReviewService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Review>> GetAllReviewsAsync()
    {
        return await _context.Reviews
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetApprovedReviewsAsync()
    {
        return await _context.Reviews
            .Where(r => r.IsApproved)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetPendingReviewsAsync()
    {
        return await _context.Reviews
            .Where(r => !r.IsApproved)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<Review?> GetReviewByIdAsync(int id)
    {
        return await _context.Reviews.FindAsync(id);
    }

    public async Task<bool> CreateReviewAsync(Review review)
    {
        try
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating review");
            return false;
        }
    }

    public async Task<bool> ApproveReviewAsync(int id)
    {
        try
        {
            var review = await GetReviewByIdAsync(id);
            if (review == null) return false;
            
            review.IsApproved = true;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error approving review");
            return false;
        }
    }

    public async Task<bool> DeleteReviewAsync(int id)
    {
        try
        {
            var review = await GetReviewByIdAsync(id);
            if (review == null) return false;
            
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting review");
            return false;
        }
    }
}