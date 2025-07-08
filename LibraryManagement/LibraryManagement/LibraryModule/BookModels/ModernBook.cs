namespace LibraryManagement.LibraryModule.BookModels
{
    public class ModernBook : BookBase
    {
        public bool IsDigital { get; set; } = false;
        public override string BookInfo() => $"{Title} - {Author}; Цифровая копия?: {(IsDigital ? "Да" : "Нет")}; " +
            $"Год: {Publication}; Код книги: {Serial}";
        public override void UpdateInfo(IBook Other)
        {
            base.UpdateInfo(Other);
            if (Other is ModernBook mb)
            {
                Category = BookCategory.Modern;
                IsDigital = mb.IsDigital;
            }
        }
        public override string GetShortDescription() => $"{Title} - {Author} [Цифровая копия?: {(IsDigital ? "Да" : "Нет")}]";
    }
}
