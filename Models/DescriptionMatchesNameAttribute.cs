using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProductsValidation.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DescriptionMatchesNameAttribute : ValidationAttribute
    {
        private static readonly ConcurrentDictionary<(Type Type, string PropertyName), PropertyInfo> PropertyCache = new();
        private readonly string targetPropertyName;
        private string fallbackErrorMessage;

        public DescriptionMatchesNameAttribute(string targetPropertyName)
        {
            if (string.IsNullOrWhiteSpace(targetPropertyName))
            {
                throw new ArgumentException("Target property name must be provided.", nameof(targetPropertyName));
            }

            this.targetPropertyName = targetPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext?.ObjectInstance == null)
            {
                return ValidationResult.Success;
            }

            var description = value as string;
            if (string.IsNullOrWhiteSpace(description))
            {
                return ValidationResult.Success;
            }

            if (description.Length <= 2)
            {
                return CreateValidationResult(validationContext, "Description length should be longer than 2 symbols.");
            }

            var nameProperty = PropertyCache.GetOrAdd(
                (validationContext.ObjectType, targetPropertyName),
                key => key.Type.GetProperty(key.PropertyName));

            if (nameProperty == null)
            {
                throw new InvalidOperationException(
                    $"Property '{targetPropertyName}' was not found on type '{validationContext.ObjectType.FullName}'.");
            }

            var name = nameProperty.GetValue(validationContext.ObjectInstance) as string;

            if (description == name)
            {
                return CreateValidationResult(validationContext, "Description should not be equal to Name.");
            }

            if (!string.IsNullOrWhiteSpace(name) && !description.StartsWith(name, StringComparison.Ordinal))
            {
                return CreateValidationResult(validationContext, "Description should start with product Name.");
            }

            return ValidationResult.Success;
        }

        private ValidationResult CreateValidationResult(ValidationContext validationContext, string defaultMessage)
        {
            fallbackErrorMessage = defaultMessage;
            var message = FormatErrorMessage(validationContext.DisplayName);

            return new ValidationResult(message, new[] { validationContext.MemberName });
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrWhiteSpace(ErrorMessage)
                ? fallbackErrorMessage
                : base.FormatErrorMessage(name);
        }
    }
}
