using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Domain.Common.Entities
{
    public abstract record BaseDomainResponse(int StatusCode, string Message)
    {
        public static string EnsureMessages(string messages, string defaultMessage)
        => messages.Length > 0 ? messages : defaultMessage;
    }

    public record DomainError(int StatusCode, string Message) : BaseDomainResponse(StatusCode, Message)
    {
        public static DomainError NotFound(string message = "The requested resource was not found.")
            => new(StatusCodes.Status404NotFound, EnsureMessages(message, "The requested resource was not found."));

        public static DomainError ServerError(string message = "An unexpected error occurred on the server.")
            => new(StatusCodes.Status500InternalServerError, EnsureMessages(message, "An unexpected error occurred on the server."));

        public static DomainError BadRequest(string message = "The request could not be understood or was missing required parameters.")
            => new(StatusCodes.Status400BadRequest, EnsureMessages(message, "The request could not be understood or was missing required parameters."));

        public static DomainError Unauthorized(string message = "Authentication is required and has failed or has not yet been provided.")
            => new(StatusCodes.Status401Unauthorized, EnsureMessages(message, "Authentication is required and has failed or has not yet been provided."));

        public static DomainError Forbidden(string message = "The request was valid, but the server is refusing action.")
            => new(StatusCodes.Status403Forbidden, EnsureMessages(message, "The request was valid, but the server is refusing action."));
    }

    public record DomainSuccess(int StatusCode, string Message) : BaseDomainResponse(StatusCode, Message)
    {
        public static DomainSuccess OK(string message = "OK")
            => new(StatusCodes.Status200OK, EnsureMessages(message, "OK"));

        public static DomainSuccess Created(string message = "Created")
            => new(StatusCodes.Status201Created, EnsureMessages(message, "Created"));

        public static DomainSuccess Accepted(string message = "Accepted")
            => new(StatusCodes.Status202Accepted, EnsureMessages(message, "Accepted"));

        public static DomainSuccess NoContent(string message = "NoContent")
            => new(StatusCodes.Status204NoContent, EnsureMessages(message, "NoContent"));
    }

    public record DomainSuccess<T>(int StatusCode, T ResponseValue, string Message) : DomainSuccess(StatusCode, Message)
    {
        public static DomainSuccess<T> OK(T responseValue, string message = "OK")
            => new(StatusCodes.Status200OK, responseValue, EnsureMessages(message, "OK"));

        public static DomainSuccess<T> Created(T responseValue, string message = "Created")
            => new(StatusCodes.Status201Created, responseValue, EnsureMessages(message, "Created"));

        public static DomainSuccess<T> Accepted(T responseValue, string message = "Accepted")
            => new(StatusCodes.Status202Accepted, responseValue, EnsureMessages(message, "Accepted"));

        public static DomainSuccess<T> NoContent(T responseValue, string message = "NoContent")
            => new(StatusCodes.Status204NoContent, responseValue, EnsureMessages(message, "NoContent"));

    }
  


}