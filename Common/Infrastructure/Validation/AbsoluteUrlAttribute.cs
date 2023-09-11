using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Infrastructure.Validation
{
    public sealed class AbsoluteUrlAttribute : ValidationAttribute
    {
        public AbsoluteUrlAttribute()
            : base(() => "The {0} field must be an absolute URL.")
        {
        }

        public override bool IsValid(object value)
        {
            return !(value is Uri uri) || uri.IsAbsoluteUri;
        }
    }
}
