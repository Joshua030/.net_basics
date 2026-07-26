using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.Exceptions
{
    public class ConflictException : Exception
    {
        public List<string> ErrorMessages { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ConflictException(List<string> errorMessages = default, HttpStatusCode statusCode = HttpStatusCode.Conflict)
        {
            StatusCode = statusCode;
            ErrorMessages = errorMessages;
        }
    }
}
