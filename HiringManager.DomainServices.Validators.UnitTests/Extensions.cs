using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Simple.Validation;

namespace HiringManager.DomainServices.Validators.UnitTests
{
    public static class Extensions
    {
        public static void AssertIsValidFor(this IEnumerable<ValidationResult> self, ValidationResult template, string format = null, params object[] args)
        {
            var query = GetValidationResultQueryFromTemplate(self, template);

            if (query.Any())
            {
                if (!string.IsNullOrWhiteSpace(format))
                {
                    var formattedMessage = string.Format(format, args);
                    Assert.Fail(formattedMessage);
                }
                else
                {
                    var message = "Did not expect an error.";
                    Assert.Fail(message);
                }
            }
        }

        public static void AssertInvalidFor(this IEnumerable<ValidationResult> self, ValidationResult template, string format = null, params object[] args)
        {
            var query = GetValidationResultQueryFromTemplate(self, template).ToList();
            Console.WriteLine("{0} validation results found matching the template.", query.Count);


            if (!query.Any())
            {
                if (!string.IsNullOrWhiteSpace(format))
                {
                    var formattedMessage = string.Format(format, args);
                    Assert.Fail(formattedMessage);
                }
                else
                {
                    var message = "Expected an error.";
                    Assert.Fail(message);
                }
            }
        }

        private static IEnumerable<ValidationResult> GetValidationResultQueryFromTemplate(IEnumerable<ValidationResult> self, ValidationResult template)
        {
            Func<ValidationResult, bool> propertyNamePredicate =
                row =>
                {
                    if (!string.IsNullOrWhiteSpace(template.PropertyName))
                        return row.PropertyName == template.PropertyName;

                    return true;
                };

            Func<ValidationResult, bool> contextPredicate =
                row =>
                {
                    if (template.Context != null)
                        return row.Context == template.Context;
                    return true;
                };

            Func<ValidationResult, bool> severityPredicate =
                row => row.Severity == template.Severity;

            Func<ValidationResult, bool> messagePredicate =
                row => string.IsNullOrWhiteSpace(template.Message) || row.Message.Contains(template.Message);

            Func<ValidationResult, bool> typePredicate =
                row => row.Type == template.Type;

            var query = self
                .Where(propertyNamePredicate)
                .Where(contextPredicate)
                .Where(severityPredicate)
                .Where(messagePredicate)
                .Where(typePredicate)
                .ToList()
                ;
            return query;
        }
    }
}