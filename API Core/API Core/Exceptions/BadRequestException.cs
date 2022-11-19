using System;

namespace API_Core.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string name, int key) : base($"{name}({key}) || Bad Request")
        {

        }
    } 
}
