using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagment.SharedLibrary.Exceptions
{
    public class NotFoundRequestException : Exception
    {
        public NotFoundRequestException(string? message) : base(message)
        {
        }
    }
}
