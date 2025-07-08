using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.LibraryModule.BookModels
{
    public class GuideBook : BookBase
    {
        [RegularExpression(@"^[0-9]{6,12}$", ErrorMessage = "<PhoneData> только цифрами, длина 6-12 символов.")]
        public long PhoneData { get; set; }

        private string FormatPhoneNumber(long phone) => phone == 0 ? "" : phone.ToString();

        public override string BookInfo() => $"{Title} - {Author}; Тел. номер: {FormatPhoneNumber(PhoneData)}; Год: {Publication};" +
            $" Код книги: {Serial}";
        public override void UpdateInfo(IBook Other)
        {
            base.UpdateInfo(Other);
            if (Other is GuideBook gb)
            {
                Category = BookCategory.Guide;
                PhoneData = gb.PhoneData;
            }
        }
        public override string GetShortDescription() => $"{Title} - {Author} ({FormatPhoneNumber(PhoneData)})";
    }
}
