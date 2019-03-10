using System;
using System.Runtime.Serialization;

namespace PictureManager.Exceptions
{
    [Serializable]
#pragma warning disable S4027 // Exceptions should provide standard constructors - Abstract, base exception, public constructors aren't needed
    public abstract class BaseCustomException : Exception
#pragma warning restore S4027 // Exceptions should provide standard constructors - Abstract, base exception, public constructors aren't needed
    {
        protected BaseCustomException() { }
        protected BaseCustomException(string message) : base(message) { }
        protected BaseCustomException(string message, Exception inner) : base(message, inner) { }
        protected BaseCustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public abstract string Description { get; }
    }

    [Serializable]
    public class EntityDoesNotExistException : BaseCustomException
    {
        public EntityDoesNotExistException() { }
        public EntityDoesNotExistException(string message) : base(message) { }
        public EntityDoesNotExistException(string message, Exception inner) : base(message, inner) { }
        protected EntityDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string Description => "Non existing entity";
    }

    [Serializable]
    public class InternalErrorException : BaseCustomException
    {
        public InternalErrorException() { }
        public InternalErrorException(string message) : base(message) { }
        public InternalErrorException(string message, Exception inner) : base(message, inner) { }
        protected InternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string Description => "Internal error exception";
    }
}
