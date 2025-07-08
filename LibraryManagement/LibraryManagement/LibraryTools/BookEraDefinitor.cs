using LibraryManagement.LibraryModule;

namespace LibraryManagement.LibraryTools
{
    public static class BookEraDefinitor
    {
        public static BookEra MatchEra(int year)
        {
            return year switch
            {
                <= 1999 => BookEra.PreDigits,
                2000 => BookEra.Early2000s,
                >= 2001 and <= 2010 => BookEra.From2000To2010,
                >= 2011 and <= 2019 => BookEra.GenZ,
                >= 2020 and <= 2023 => BookEra.PostCovid,
                _ => BookEra.Nowadays
            };
        }
        public static bool IsCategoryAllowed(BookCategory category, BookEra era)
        {
            return category switch
            {
                BookCategory.Historical => era == BookEra.PreDigits,
                BookCategory.Guide => era != BookEra.PreDigits,
                BookCategory.Modern => era is BookEra.GenZ or BookEra.PostCovid or BookEra.Nowadays,
                _ => false
            };
        }
        public static string EraLabeler(BookEra era) => era switch 
        { 
            BookEra.PreDigits => "Доцифровая эпоха",
            BookEra.Early2000s => "Ранние 2000",
            BookEra.From2000To2010 => "2000-2010",
            BookEra.GenZ => "Поколение Z",
            BookEra.PostCovid => "Пост-Ковидное время",
            BookEra.Nowadays => "Современность",
            _ => "Неизвестная эпоха"
        };
    }
}
