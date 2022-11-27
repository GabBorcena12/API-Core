using System.Threading.Tasks;
using System;
using API_Core.Model.Models;

namespace API_Core.Interface
{
    public interface ILogging
    {
        public void Log(ErrorDetails errorDetails); 
    }
}
