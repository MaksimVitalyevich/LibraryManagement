using LibraryManagement.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult StatusCodeHandler(int statusCode)
        {
            var errorModel = new ErrorViewModel
            {
                Code = statusCode,
                Message = statusCode switch
                {
                    400 => "Некорректный запрос.",
                    404 => "Страница не найдена.",
                    415 => "Неверный формат данных запроса.",
                    500 => "Внутренняя ошибка сервера.",
                    502 => "Ошибка подключения.",
                    _ => "Неизвестная ошибка."
                },
                RequestId = HttpContext.TraceIdentifier
            };

            return View("Error", errorModel);
        }

        [Route("Error/Exception")]
        public IActionResult ExceptionHandler()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var message = TempData["Error"]?.ToString() ?? exceptionFeature?.Error.Message ?? "Исключение без описания.";

            var errorModel = new ErrorViewModel
            {
                Code = "ИСКЛЮЧЕНИЕ",
                Message = message,
                RequestId = HttpContext.TraceIdentifier
            };

            return View("Error", errorModel);
        }
    }
}
