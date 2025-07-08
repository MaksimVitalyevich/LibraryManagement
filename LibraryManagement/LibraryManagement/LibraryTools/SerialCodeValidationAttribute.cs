using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.LibraryTools
{
    public class SerialCodeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var serial = value as string;

            if (string.IsNullOrEmpty(serial))
                return ValidationResult.Success;

            if (serial.All(char.IsDigit))
                return new ValidationResult("<Serial> Не может содержать одни цифры.");

            int letterCount = serial.Count(char.IsLetter);
            if (letterCount < 3)
                return new ValidationResult("<Serial> Должен иметь не менее 3 букв.");

            return ValidationResult.Success;
        }
    }
}
