using LibraryManagement.LibraryModule;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class LibraryListController(IUnifiedBookService bookService) : Controller
    {
        private readonly IUnifiedBookService _bookService = bookService;

        public IActionResult Index(BookCategory? selectedCategory, BookEra? selectedEra)
        {
            var user = AccountController.getCurrentUser(HttpContext);
            var allBooks = _bookService.GetAllBooks();

            if (string.IsNullOrEmpty(user))
                RedirectToAction("Login", "Account");

            if (selectedCategory.HasValue)
                allBooks = allBooks.Where(b => b.Category == selectedCategory.Value).ToList();

            if (selectedEra.HasValue)
                allBooks = allBooks.Where(b => b.Era == selectedEra.Value).ToList();

            var listModel = new LibraryListModel
            {
                Books = allBooks,
                SelectedCategory = selectedCategory,
                SelectedEra = selectedEra
            };

            return View(listModel);
        }

        public IActionResult BookDetails(int year, string title)
        {
            var book = _bookService.FindBook(year, title);

            if (book is null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(int year, string title)
        {
            var success = _bookService.DeleteBook(year, title);

            if (!success)
                return RedirectToAction("Error");

            return RedirectToAction("Index", "LibraryList");
        }
    }
}
