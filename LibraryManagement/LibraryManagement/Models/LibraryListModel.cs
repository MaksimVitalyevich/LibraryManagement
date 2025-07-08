using LibraryManagement.LibraryModule;

namespace LibraryManagement.Models
{
    public class LibraryListModel
    {
        public List<BookBase> Books { get; set; }
        public BookCategory? SelectedCategory { get; set; }
        public BookEra? SelectedEra { get; set; }
    }
}
