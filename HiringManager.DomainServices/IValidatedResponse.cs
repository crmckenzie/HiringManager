using System.Collections.Generic;
using Simple.Validation;

namespace HiringManager.DomainServices
{
    public interface IValidatedResponse
    {
        IEnumerable<ValidationResult> ValidationResults { get; set; }
    }
}