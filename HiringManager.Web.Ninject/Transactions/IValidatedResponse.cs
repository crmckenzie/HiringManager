using System.Collections.Generic;
using Simple.Validation;

namespace HiringManager.Web.Ninject.Transactions
{
    public interface IValidatedResponse
    {
        IEnumerable<ValidationResult> ValidationResults { get; set; }
    }
}