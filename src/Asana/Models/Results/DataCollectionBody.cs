using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Asana.Models.Results
{
    internal sealed class DataCollectionBody<TData> where TData : IData
    {
        public TData[] Data { get; }
        [JsonProperty("next_page")]
        public NextPageInformation? NextPage { get; }

        public DataCollectionBody(IEnumerable<TData> data) : this(data, null)
        {
        }

        [JsonConstructor]
        public DataCollectionBody(IEnumerable<TData> data, NextPageInformation? nextPage)
        {
            NextPage = nextPage;
            Data = data?.ToArray() ?? new TData[0];
        }
    }
}