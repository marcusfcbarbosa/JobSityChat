using System;
using System.Net;

namespace JobSityChat.Extensions
{
    public class CustomHttpRequestException : Exception
    {
        public HttpStatusCode _statusCode;
        public CustomHttpRequestException()
        {
        }
        public CustomHttpRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public CustomHttpRequestException(HttpStatusCode StatusCode)
        {
            _statusCode = StatusCode;
        }
    }
}
