using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("Bad request.") { }
        public BadRequestException(string message) : base(message) { }
    }
}