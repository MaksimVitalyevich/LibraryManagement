using LibraryManagement.LibraryModule;
using LibraryManagement.LibraryTools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.Controllers
{
    public abstract class BaseBookController<TModel>(IBookService<TModel> service) : Controller
        where TModel : BookBase, new()
    {
        protected readonly IBookService<TModel> _service = service;

        public IEnumerable<SelectListItem> GetAllowedEras(BookCategory category)
        {
            var all = Enum.GetValues<BookEra>().Cast<BookEra>();

            return all.Where(era => BookEraDefinitor.IsCategoryAllowed(category, era)).Select(era => new SelectListItem
            {
                Text = BookEraDefinitor.EraLabeler(era),
                Value = era.ToString()
            }).ToList();
        }
        [HttpPost]
        public abstract IActionResult FormEdit(TModel model);
        public abstract IActionResult FormEdit(int year, string title);
        public virtual IActionResult Reset() => RedirectToAction("FormEdit");
    }
}
