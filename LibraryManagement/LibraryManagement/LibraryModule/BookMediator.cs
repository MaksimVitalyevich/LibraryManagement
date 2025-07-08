namespace LibraryManagement.LibraryModule
{
    public class BookMediator(IEnumerable<IBookService<IBook>> services) : IUnifiedBookService
    {
        private readonly List<IBookService<IBook>> _services = services.ToList();

        public List<BookBase> GetAllBooks()
        {
            return _services.SelectMany(s => s.GetBooks()).Cast<BookBase>().ToList();
        }

        public BookBase? FindBook(int year, string title)
        {
            foreach (var service in _services)
            {
                var book = service.GetBook(year, title);
                if (book != null) return book as BookBase;
            }

            return null;
        }

        public bool DeleteBook(int year, string title)
        {
            foreach (var service in _services)
                if (service.RemoveBook(year, title)) return true;

            return false;
        }
    }
}
