namespace LibraryManagement.LibraryModule
{
    public class BookServiceAdapter<T>(IBookService<T> innerService) : IBookService<IBook> 
        where T : class, IBook
    {
        private readonly IBookService<T> _innerService = innerService;

        public List<IBook> Books 
        { 
            get => _innerService.Books.Cast<IBook>().ToList(); 
            set => _innerService.Books = value.Cast<T>().ToList();
        }

        public List<IBook> GetBooks() => _innerService.GetBooks().Cast<IBook>().ToList();
        public IBook? GetBook(int year, string title) => _innerService.GetBook(year, title);
        public IBook AddNewBook(IBook book) => _innerService.AddNewBook((T)book);
        public bool UpdateBook(IBook book) => _innerService.UpdateBook((T)book);
        public bool RemoveBook(int year, string title) => _innerService.RemoveBook(year, title);

        public bool CanHandle(Type bookType) => bookType == typeof(T);
    }
}
