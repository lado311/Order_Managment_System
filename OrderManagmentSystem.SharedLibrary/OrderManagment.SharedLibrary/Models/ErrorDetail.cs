using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderManagment.SharedLibrary.Models
{
    internal class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrorId { get; set; } = Guid.NewGuid().ToString();
        public string RequestId { get; set; }
        public string Details { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public string StackTrace { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true // To make the JSON output more readable
            });
        }
    }
}
