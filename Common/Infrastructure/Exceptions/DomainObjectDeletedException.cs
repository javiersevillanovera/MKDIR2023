﻿using System;
using System.Runtime.Serialization;
using Common.Infrastructure;

namespace Decode.OP.Common.Infrastructure
{
    [Serializable]
    public class DomainObjectDeletedException : DomainObjectException
    {
        public DomainObjectDeletedException(string id, Type type)
            : base(FormatMessage(id, type), id, type)
        {
        }

        protected DomainObjectDeletedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string FormatMessage(string id, Type type)
        {
            return $"Domain object \'{id}\' (type {type}) already deleted.";
        }
    }
}
