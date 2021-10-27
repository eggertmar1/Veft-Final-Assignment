using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() : base("Resource was not found.") { }
        public ResourceNotFoundException(string entity, string type, string identifier) : base($"{entity} with {type}: \"{identifier}\" was not found.") { }
        public ResourceNotFoundException(string message) : base(message) { }
    }
}