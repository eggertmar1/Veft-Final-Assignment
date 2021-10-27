using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException() : base("Invalid request.") { }
        public InvalidRequestException(string message) : base(message) { }
    }
}