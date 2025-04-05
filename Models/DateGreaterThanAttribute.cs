namespace API_FEB.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success; // Let [Required] handle nulls if needed

            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (comparisonProperty == null)
            {
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var currentValue = (DateTime)value;
            var comparisonValue = (DateTime)comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (currentValue <= comparisonValue)
            {
                var displayName = validationContext.DisplayName;
                return new ValidationResult(ErrorMessage ?? $"{displayName} must be later than {_comparisonProperty}.");
            }

            return ValidationResult.Success;
        }
    }
}
