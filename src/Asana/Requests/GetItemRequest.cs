﻿using System.Net.Http;
using Asana.Models;

namespace Asana.Requests
{
    public sealed class GetItemRequest<TData> : ItemRequest<TData> where TData : class, IData
    {
        protected override HttpMethod HttpMethod => HttpMethod.Get;

        public GetItemRequest(Dispatcher dispatcher, string requestPath) : base(dispatcher, requestPath)
        {
        }


        public new GetItemRequest<TData> AddQueryParameter(string name, string value) => (GetItemRequest<TData>)base.AddQueryParameter(name, value);

    }
}