using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ArtTalesFull.Attributes
{
    public class AttachmentAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Attributes.ContainsKey("data-val"))
            {
                context.Attributes.Add("data-val", "true");
            }


            if (!context.Attributes.ContainsKey("data-val-required"))
            {
                context.Attributes.Add("data-val-required", ErrorMessage!);
            }
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {

            if (value != null && ((List<IFormFile>)value).Count() > 0 && ((List<IFormFile>)value).Count() < 13)
            {
                return ValidationResult.Success!;
            }

            return new ValidationResult(this.ErrorMessage);
        }
    }
}