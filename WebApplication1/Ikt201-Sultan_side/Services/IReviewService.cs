using Ikt201_Sultan_side.Models;

namespace Ikt201_Sultan_side.Services;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetAllReviewsAsync();
    Task<IEnumerable<Review>> GetApprovedReviewsAsync();
    Task<IEnumerable<Review>> GetPendingReviewsAsync();
    Task<Review?> GetReviewByIdAsync(int id);
    Task<bool> CreateReviewAsync(Review review);
    Task<bool> ApproveReviewAsync(int id);
    Task<bool> DeleteReviewAsync(int id);
}