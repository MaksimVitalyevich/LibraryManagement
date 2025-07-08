namespace LibraryManagement.LibraryModule
{
    public abstract class AbstractBookService<TModel> : IBookService<TModel> where TModel : class, IBook, new()
    {
        public List<TModel> Books { get; set; } = new();

        public virtual List<TModel> GetBooks() => Books;
        public virtual TModel? GetBook(int year, string title) => 
            Books.FirstOrDefault(b => b.Publication == year && b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        public abstract TModel AddNewBook(TModel book);
        public abstract bool UpdateBook(TModel book);
        public abstract bool RemoveBook(int year, string title);

        public virtual bool CanHandle(Type bookType) => bookType == typeof(TModel);
    }
}
