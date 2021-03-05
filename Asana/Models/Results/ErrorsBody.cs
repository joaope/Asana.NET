using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Asana.Models.Results
{
    internal sealed class ErrorsBody
    {
        public Error[] Errors { get; }

        [JsonConstructor]
        public ErrorsBody(IEnumerable<Error> errors)
        {
            Errors = errors?.ToArray() ?? new Error[0];
        }
    }
}