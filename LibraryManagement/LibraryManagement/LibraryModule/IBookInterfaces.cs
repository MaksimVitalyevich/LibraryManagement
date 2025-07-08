namespace LibraryManagement.LibraryModule
{
    public interface IBook
    {
        BookCategory Category { get; set; }
        BookEra Era { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        int Publication { get; set; }
        string Serial { get; set; }
    }
    public interface IBookService<TModel> where TModel : IBook
    {
        List<TModel> Books { get; set; }
        List<TModel> GetBooks();
        TModel? GetBook(int year, string title);
        TModel AddNewBook(TModel book);
        bool UpdateBook(TModel book);
        bool RemoveBook(int year, string title);
        bool CanHandle(Type bookType);
    }
    public interface IUnifiedBookService
    {
        List<BookBase> GetAllBooks();
        BookBase? FindBook(int year, string title);
        bool DeleteBook(int year, string title);
    }
}