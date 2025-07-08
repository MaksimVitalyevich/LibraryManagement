using LibraryManagement.LibraryTools;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.LibraryModule
{
    public enum BookCategory { Historical, Guide, Modern }
    public enum BookEra { PreDigits, Early2000s, From2000To2010, GenZ, PostCovid, Nowadays}
    public abstract class BookBase : IBook
    {
        public string Title { get; set; }
        public string Author { get; set; } = "Неизвестный";
        public int Publication { get; set; }

        [RegularExpression(@"^[A-Z0-9]{5,10}$", ErrorMessage = "<Serial> должен быть только заглавными латинскими буквами не менее 3, " +
            "длина 5-10 символов.")]
        [SerialCodeValidation]
        public string Serial { get; set; }
        public BookEra Era { get; set; }
        public BookCategory Category { get; set; }

        public virtual string BookInfo() => $"{Title} - {Author}; Год: {Publication}; Код книги: {Serial}";
        public virtual void UpdateInfo(IBook Other)
        {
            Title = Other.Title;
            Author = Other.Author;
            Publication = Other.Publication;
            Serial = Other.Serial;
        }
        public virtual string GetShortDescription() => $"{Title} - {Author} ({Publication})";
    }
}
