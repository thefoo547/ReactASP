using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App.ErrorHandlers
{
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public Object Error { get; }

        public BusinessException(HttpStatusCode statusCode, Object error)
        {
            StatusCode = statusCode;
            Error = error;
        }
    }
}
