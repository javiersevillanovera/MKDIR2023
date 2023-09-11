using System;
using System.Runtime.Serialization;

namespace Common.Infrastructure
{
    [Serializable]
    public class DomainForbiddenException : DomainException
    {
        public DomainForbiddenException(string message)
            : base(message)
        {
        }

        public DomainForbiddenException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DomainForbiddenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
