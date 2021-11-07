using System;
using System.Collections.Generic;
using System.Linq;

namespace OngProject.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base() {}
        
        public BadRequestException(string message) : base(message) {}
        
        public BadRequestException(IEnumerable<string> message) : base($"{String.Join(" ", message.ToList())}") {}
    }
}