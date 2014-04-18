using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Simple.Validation;

namespace HiringManager.Web.Infrastructure.MVC
{
    public static class ModelStateExtensions
    {
        public static void AddModelErrors(this ModelStateDictionary dictionary, IEnumerable<ValidationResult> validationResults)
        {
            foreach (var validationResult in validationResults)
            {
                dictionary.AddModelError(validationResult.PropertyName, validationResult.Message);
            }
        }
    }
}