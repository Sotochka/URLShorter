using System.Net;

namespace URLShorter.Backend.Common.Middleware.ExceptionHandler;

public class BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}