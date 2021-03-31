using System;

namespace Asana.OAuth
{
    public sealed class AsanaOAuthException : Exception
    {
        public string? Error { get; }
        public string? ErrorDescription { get; }
        public string? HttpErrorReason { get; }
        public ResponseErrorType ErrorType { get; }

        internal AsanaOAuthException(
            string message,
            string error,
            string httpErrorReason,
            IdentityModel.Client.ResponseErrorType errorType,
            Exception exception) : this(message, error, null, httpErrorReason, errorType, exception)
        {
        }

        internal AsanaOAuthException(
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

        internal AsanaOAuthException(string message) : base(message) { }

        public enum ResponseErrorType
        {
            /// <summary>none</summary>
            None,
            /// <summary>
            /// protocol related - valid response, but some protocol level error.
            /// </summary>
            Protocol,
            /// <summary>HTTP error - e.g. 404.</summary>
            Http,
            /// <summary>
            /// An exception occurred - exception while connecting to the endpoint, e.g. TLS problems.
            /// </summary>
            Exception,
            /// <summary>
            /// A policy violation - a configured policy was violated.
            /// </summary>
            PolicyViolation,
        }
    }
}