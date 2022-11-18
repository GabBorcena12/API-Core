using System;

namespace API_Core.Exceptions
{
    public class NotFoundException:ApplicationException
    {
        public NotFoundException(string name,int key):base($"{name}({key}) was not found ")
        {

        }
    }
}
