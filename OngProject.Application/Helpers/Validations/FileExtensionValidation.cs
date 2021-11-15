using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OngProject.Application.Helpers.Enums;
using System.Linq;

namespace OngProject.Application.Helpers.Validations
{
    public class FileExtensionValidation : ValidationAttribute
    {
        private readonly string[] _fileExtensions;

        public FileExtensionValidation(string[] fileExtensions)
        {
            _fileExtensions = fileExtensions;
        }

        public FileExtensionValidation(FileType fileType)
        {
            if (fileType == FileType.Image)
                _fileExtensions = new string[] { "image/png", "image/jpeg" };
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile is null)
                return ValidationResult.Success;

            if (!_fileExtensions.Contains(formFile.ContentType))
                return new ValidationResult($"This field only accepts files with the following extensions: {String.Join(", ", _fileExtensions)}");

            return ValidationResult.Success;
        }
    }
}