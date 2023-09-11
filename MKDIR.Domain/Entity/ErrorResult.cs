using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public class ErrorResult
    {
        public int? ErrorCode { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public ErrorResult() { }
        public ErrorResult(int errorCode) => ErrorCode = errorCode;
        public ErrorResult(string message) => Message = message;
        public ErrorResult(Exception exception) => Exception = exception;
        public ErrorResult(int errorCode, string message, Exception exception)
        {
            ErrorCode = errorCode;
            Message = message;
            Exception = exception;
        }
    }
}
