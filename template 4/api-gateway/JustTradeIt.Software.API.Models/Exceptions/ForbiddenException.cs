using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Unauthorized.") { }
        public ForbiddenException(string message) : base(message) { }
    }
}