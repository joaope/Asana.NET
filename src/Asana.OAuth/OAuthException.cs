using System;

namespace Asana.OAuth
{
    public sealed class OAuthException : Exception
    {
        public string? Error { get; }
        public string? ErrorDescription { get; }
        public string? HttpErrorReason { get; }
        public ResponseErrorType ErrorType { get; }

        internal OAuthException(
            string message,
            string error,
            string httpErrorReason,
            IdentityModel.Client.ResponseErrorType errorType,
            Exception exception) : this(message, error, null, httpErrorReason, errorType, exception)
        {
        }

        internal OAuthException(
            string message,
            string? error,
            string? errorDescription,
            string? httpErrorReason,
            IdentityModel.Client.ResponseErrorType errorType,
            Exception exception) 
            : base(message, exception)
        {
            Error = error;
            ErrorDescription = errorDescription;
            HttpErrorReason = httpErrorReason;
            ErrorType = (ResponseErrorType)errorType;
        }

        internal OAuthException(string message) : base(message) { }
    }
}