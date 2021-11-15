using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Application.Helpers.Validations
{
    public class FileSizeValidation : ValidationAttribute
    {
        private readonly int _maxSizeInMegabytes;

        public FileSizeValidation(int maxSizeInMegabytes)
        {
            _maxSizeInMegabytes = maxSizeInMegabytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile is null)
                return ValidationResult.Success;

            if (formFile.Length > _maxSizeInMegabytes * 1024 * 1024)
                return new ValidationResult($"Maximum allowed file size is {_maxSizeInMegabytes} MB.");

            return ValidationResult.Success;
        }
    }
}