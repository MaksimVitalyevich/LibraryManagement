using LibraryManagement.ReviewModule;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ReviewController(IDataStorageService reviewService) : Controller
    {
        private readonly IDataStorageService _reviewStorage = reviewService;

        public IActionResult Index()
        {
            var reviews = _reviewStorage.GetReviews();
            return View(reviews);
        }

        [HttpPost]
        public IActionResult Add(Review review)
        {
            if (!ModelState.IsValid)
                return View("Index", _reviewStorage.GetReviews());

            var user = AccountController.getCurrentUser(HttpContext) ?? "Гость";
            _reviewStorage.AddReview(review, user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddComment(Guid reviewId, Comment comment)
        {
            var user = AccountController.getCurrentUser(HttpContext) ?? "Гость";
            _reviewStorage.AddComment(reviewId, comment, user);
            return RedirectToAction("Details", new { id = reviewId });
        }

        public IActionResult Details(Guid id)
        {
            var review = _reviewStorage.GetReview(id);
            if (review == null) return NotFound();
            return View(review);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _reviewStorage.DeleteReview(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LikeComment(Guid reviewId, Guid commentId)
        {
            _reviewStorage.LikeComment(reviewId, commentId);
            return RedirectToAction("Details", new { id = reviewId });
        }

        [HttpPost]
        public IActionResult DislikeComment(Guid reviewId, Guid commentId)
        {
            _reviewStorage.DislikeComment(reviewId, commentId);
            return RedirectToAction("Details", new { id = reviewId });
        }
    }
}
