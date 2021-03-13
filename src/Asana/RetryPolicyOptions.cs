﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Asana
{
    public readonly struct RetryPolicyOptions
    {
        public HttpStatusCode[] HttpStatusCodes { get; }
        public int MaxRetries { get; }
        public TimeSpan PollInterval { get; }

        public RetryPolicyOptions(IEnumerable<HttpStatusCode> httpStatusCodes, int maxRetries, TimeSpan pollInterval)
        {
            HttpStatusCodes = httpStatusCodes?.ToArray() ?? new HttpStatusCode[0];
            MaxRetries = maxRetries;
            PollInterval = pollInterval;
        }

        public static RetryPolicyOptions Default =>
            new RetryPolicyOptions(new[] {HttpStatusCode.Unauthorized}, 3, TimeSpan.FromMilliseconds(3));
    }
}