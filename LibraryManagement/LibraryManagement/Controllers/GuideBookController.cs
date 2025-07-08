using LibraryManagement.LibraryModule;
using LibraryManagement.LibraryModule.BookModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class GuideBookController(IBookService<GuideBook> service) : 
        BaseBookController<GuideBook>(service)
    {
        [HttpPost]
        public override IActionResult FormEdit(GuideBook model)
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
            book ??= new GuideBook
            {
                Category = BookCategory.Guide,
                Title = title,
                Publication = year
            };

            ViewBag.AllowedEras = GetAllowedEras(BookCategory.Guide);

            return View(book);
        }
    }
}
