
using System.Text.Json;

namespace LibraryManagement.ReviewModule
{
    public class JsonDataStorageService : IDataStorageService
    {
        private readonly string _reviewsFile = "App_Data/reviews.json";
        private readonly string _usersfile = "App_Data/usernames.json";

        private List<Review> _reviews = new();
        private List<string> _users = new();

        public JsonDataStorageService() 
        {
            LoadReviews();
            LoadUsers();
        }

        public List<string> GetAllUserNames() => _users;

        public void AddUser(string username)
        {
            if (!_users.Contains(username, StringComparer.OrdinalIgnoreCase))
            {
                _users.Add(username);
                SaveUsers();
            }
        }

        public List<Review> GetReviews() => _reviews;
        public Review? GetReview(Guid id) => _reviews.FirstOrDefault(r => r.Id == id);

        public Review AddReview(Review review, string userName)
        {
            review.User = userName;
            _reviews.Add(review);
            SaveReviews();
            return review;
        }

        public bool UpdateReview(Review review)
        {
            var existing = GetReview(review.Id);
            if (existing == null) return false;

            existing.User = review.User;
            existing.Rating = review.Rating;
            existing.Text = review.Text;
            SaveReviews();
            return true;
        }

        public bool DeleteReview(Guid id)
        {
            var review = GetReview(id);
            if (review == null) return false;

            _reviews.Remove(review);
            SaveReviews();
            return true;
        }

        public Comment AddComment(Guid reviewId, Comment comment, string userName)
        {
            var review = GetReview(reviewId);
            if (review == null) throw new InvalidOperationException("Отзыв не найден.");

            comment.User = userName;
            review.Comments.Add(comment);
            SaveReviews();
            return comment;
        }

        public bool LikeComment(Guid reviewId, Guid commentId)
        {
            var comment = FindComment(reviewId, commentId);
            if (comment == null) return false;

            comment.Likes++;
            SaveReviews();
            return true;
        }

        public bool DislikeComment(Guid reviewId, Guid commentId)
        {
            var comment = FindComment(reviewId, commentId);
            if (comment == null) return false;

            comment.Dislikes++;
            SaveReviews();
            return true;
        }

        private Comment? FindComment(Guid reviewId, Guid commentId) => GetReview(reviewId)?.Comments.FirstOrDefault(c => c.Id == commentId);

        private void SaveUsers()
        {
            var dir = Path.GetDirectoryName(_usersfile);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            var json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_usersfile, json);
        }

        private void LoadUsers()
        {
            if (File.Exists(_usersfile))
            {
                var json = File.ReadAllText(_usersfile);
                _users = JsonSerializer.Deserialize<List<string>>(json) ?? new();
            }
        }

        private void SaveReviews()
        {
            var dir = Path.GetDirectoryName(_reviewsFile);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            var json = JsonSerializer.Serialize(_reviews, new JsonSerializerOptions {  WriteIndented = true });
            File.WriteAllText(_reviewsFile, json);
        }

        private void LoadReviews()
        {
            if (File.Exists(_reviewsFile))
            {
                var json = File.ReadAllText(_reviewsFile);
                _reviews = JsonSerializer.Deserialize<List<Review>>(json) ?? new();
            }
        }
    }
}
