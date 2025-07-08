using LibraryManagement.LibraryModule;
using LibraryManagement.LibraryModule.BookModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class HistoricalBookController(IBookService<HistoricalBook> service) : 
        BaseBookController<HistoricalBook>(service)
    {
        [HttpPost]
        public override IActionResult FormEdit(HistoricalBook model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            try
            {
                _service.AddNewBook(model);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ExceptionHandler", "Error");
            }

            return RedirectToAction("Index", "LibraryList");
        }

        public override IActionResult FormEdit(int year, string title)
        {
            var book = _service.GetBook(year, title);

            book ??= new HistoricalBook
            {
                 Category = BookCategory.Historical,
                 Title = title,
                 Publication = year
            };

            ViewBag.AllowedEras = GetAllowedEras(BookCategory.Historical);

            return View(book);
        }
    }
}
