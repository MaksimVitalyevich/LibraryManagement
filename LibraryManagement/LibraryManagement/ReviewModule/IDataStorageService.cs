namespace LibraryManagement.ReviewModule
{
    public interface IDataStorageService
    {
        List<string> GetAllUserNames();
        void AddUser(string username);

        List<Review> GetReviews();
        Review? GetReview(Guid id);
        Review AddReview(Review review, string userName);
        bool UpdateReview(Review review);
        bool DeleteReview(Guid id);

        Comment AddComment(Guid reviewId, Comment comment, string userName);
        bool LikeComment(Guid reviewId, Guid commentId);
        bool DislikeComment(Guid reviewId, Guid commentId);
    }
}
