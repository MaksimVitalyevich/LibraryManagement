using LibraryManagement.LibraryModule.BookModels;

namespace LibraryManagement.LibraryModule.BookServices
{
    public class HistoricalBookService : AbstractBookService<HistoricalBook>
    {
        public HistoricalBookService()
        {
            GetExistingHistoricalBooks();
        }

        private void GetExistingHistoricalBooks()
        {
            Books.Add(new()
            {
                Category = BookCategory.Historical,
                Era = BookEra.PreDigits,
                Title = "Преступление И Наказание",
                Author = "Федор Достоевский",
                Genre = "Филосовский роман",
                Publication = 1866,
                Serial = "PZ442ST81I"
            });

            Books.Add(new()
            {
                Category = BookCategory.Historical,
                Era = BookEra.PreDigits,
                Title = "Слово о полку Игореве",
                Genre = "Древнерусская поэма",
                Publication = 1185,
                Serial = "YS113FG6"
            });

            Books.Add(new()
            {
                Category = BookCategory.Historical,
                Era = BookEra.PreDigits,
                Title = "Русь изначальная",
                Author = "Валентин Иванов",
                Genre = "Древнерусская поэма",
                Publication = 1206,
                Serial = "O90JF253L"
            });
        }
        public override List<HistoricalBook> GetBooks() => base.GetBooks();
        public override HistoricalBook AddNewBook(HistoricalBook book)
        {
            book.Category = BookCategory.Historical;
            book.Era = BookEra.PreDigits;

            if (GetBook(book.Publication, book.Title) is not null)
                throw new InvalidOperationException("Книга с таким названием и годом публикации уже существует.");

            Books.Add(book);
            return book;
        }
        public override bool UpdateBook(HistoricalBook book)
        {
            var changedbook = GetBook(book.Publication, book.Title);

            if (changedbook is not null)
            {
                changedbook.UpdateInfo(book);
                return true;
            }

            return false;
        }
        public override bool RemoveBook(int year, string title)
        {
            var removedbook = GetBook(year, title);

            if (removedbook is not null)
            {
                Books.Remove(removedbook);
                return true;
            }

            return false;
        }
    }
}
