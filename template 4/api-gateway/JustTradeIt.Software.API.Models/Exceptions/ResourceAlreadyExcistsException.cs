using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class ResourceAlreadyExistsException : Exception
    {
        public ResourceAlreadyExistsException() : base("Resource already exists.") { }
        public ResourceAlreadyExistsException(string message) : base(message) { }
    }
}