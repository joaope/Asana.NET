namespace Asana
{
    public readonly struct AsanaClientOptions
    {
        public uint? DefaultPageSize { get; }
        public RetryPolicyOptions RetryPolicy { get; }

        public AsanaClientOptions(uint? defaultPageSize, RetryPolicyOptions retryPolicy)
        {
            DefaultPageSize = defaultPageSize;
            RetryPolicy = retryPolicy;
        }
    }
}