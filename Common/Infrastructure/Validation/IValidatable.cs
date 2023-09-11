using System.Collections.Generic;

namespace Common.Infrastructure.Validation
{
    public interface IValidatable
    {
        void Validate(IList<ValidationError> errors);
    }
}
