using LibraryManagement.LibraryModule.BookModels;
using LibraryManagement.LibraryTools;

namespace LibraryManagement.LibraryModule.BookServices
{
    public class GuideBookService : AbstractBookService<GuideBook>
    {
        public GuideBookService()
        {
            GetExistingGuideBooks();
        }

        private void GetExistingGuideBooks()
        {
            Books.Add(new()
            {
                Category = BookCategory.Guide,
                Era = BookEra.GenZ,
                Title = "Руководство создания игр на C++",
                Author = "Майкл Доусон",
                PhoneData = 84992707359,
                Publication = 2016,
                Serial = "SP128HXU09"
            });

            Books.Add(new()
            {
                Category = BookCategory.Guide,
                Era = BookEra.GenZ,
                Title = "Справочник 1С",
                PhoneData = 84957379257,
                Publication = 2011,
                Serial = "S11OI62JI"
            });

            Books.Add(new()
            {
                Category = BookCategory.Guide,
                Era = BookEra.Nowadays,
                Title = "Справочник Среднее профессиональное Образование",
                PhoneData = 978560604,
                Publication = 2024,
                Serial = "Y754GH6"
            });
        }
        public override List<GuideBook> GetBooks() => base.GetBooks();
        public override GuideBook AddNewBook(GuideBook book)
        {
            book.Category = BookCategory.Guide;

            var era = BookEraDefinitor.MatchEra(book.Publication);
            book.Era = era;

            if (GetBook(book.Publication, book.Title) is not null)
                throw new InvalidOperationException("Книга с таким названием и годом публикации уже существует.");

            Books.Add(book);
            return book;
        }
        public override bool UpdateBook(GuideBook book)
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
