namespace Asana.OAuth
{
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