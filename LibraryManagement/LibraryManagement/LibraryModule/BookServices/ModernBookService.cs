using LibraryManagement.LibraryModule.BookModels;
using LibraryManagement.LibraryTools;

namespace LibraryManagement.LibraryModule.BookServices
{
    public class ModernBookService : AbstractBookService<ModernBook>
    {
        public ModernBookService() 
        {
            GetExistingNovelBooks();
        }

        private void GetExistingNovelBooks()
        {
            Books.Add(new()
            {
                Category = BookCategory.Modern,
                Era = BookEra.PostCovid,
                Title = "Красный рынок",
                Author = "Скотт Карни",
                IsDigital = true,
                Publication = 2021,
                Serial = "EU325IP72T"
            });

            Books.Add(new()
            {
                Category = BookCategory.Modern,
                Era = BookEra.Nowadays,
                Title = "Тума",
                Author = "Захар Прилепин",
                IsDigital = true,
                Publication = 2025,
                Serial = "OK317HUK36"
            });

            Books.Add(new()
            {
                Category = BookCategory.Modern,
                Era = BookEra.PostCovid,
                Title = "Путешествие в Эвелевсин",
                Author = "Виктор Пелевин",
                IsDigital = true,
                Publication = 2023,
                Serial = "6SC118KI2"
            });
        }
        public override List<ModernBook> GetBooks() => base.GetBooks();
        public override ModernBook AddNewBook(ModernBook book)
        {
            book.Category = BookCategory.Modern;

            var era = BookEraDefinitor.MatchEra(book.Publication);
            book.Era = era;

            if (GetBook(book.Publication, book.Title) is not null)
                throw new InvalidOperationException("Книга с таким названием и годом публикации уже существует.");

            Books.Add(book);
            return book;
        }
        public override bool UpdateBook(ModernBook book)
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
            var removebook = GetBook(year, title);

            if (removebook is not null)
            {
                Books.Remove(removebook);
                return true;
            }

            return false;
        }
    }
}
