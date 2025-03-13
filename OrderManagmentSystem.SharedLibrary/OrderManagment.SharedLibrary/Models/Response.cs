using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderManagment.SharedLibrary.Model
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public Response(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
    }
}
