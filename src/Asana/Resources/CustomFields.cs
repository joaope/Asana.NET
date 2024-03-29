﻿using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class CustomFields : Resource
    {
        private readonly uint? _defaultPageSize;

        internal CustomFields(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public PostItemRequest<CustomField> Create(object data)
        {
            return new PostItemRequest<CustomField>(Dispatcher, "custom_fields").AddData(data);
        }

        public GetItemRequest<CustomField> Get(string customFieldGid)
        {
            return new GetItemRequest<CustomField>(Dispatcher, $"custom_fields/{customFieldGid}");
        }

        public PutItemRequest<CustomField> Update(string customFieldGid, object data)
        {
            return new PutItemRequest<CustomField>(Dispatcher, $"custom_fields/{customFieldGid}").AddData(data);
        }

        public DeleteRequest<EmptyData> Update(string customFieldGid)
        {
            return new DeleteRequest<EmptyData>(Dispatcher, $"custom_fields/{customFieldGid}");
        }

        public GetItemsCollectionRequest<CustomField> GetWorkspaceCustomFields(string workspaceGid)
        {
            return new GetItemsCollectionRequest<CustomField>(Dispatcher, _defaultPageSize, $"workspaces/{workspaceGid}/custom_fields");
        }

        public PostItemRequest<EnumOption> CreateEnumOption(string customFieldGid, object data)
        {
            return new PostItemRequest<EnumOption>(Dispatcher, $"custom_fields/{customFieldGid}/enum_options").AddData(data);
        }

        public PostItemRequest<EnumOption> ReorderEnumOption(string customFieldGid, object data)
        {
            return new PostItemRequest<EnumOption>(Dispatcher, $"custom_fields/{customFieldGid}/enum_options/insert").AddData(data);
        }

        public PutItemRequest<EnumOption> UpdateEnumOption(string enumOptionGid, object data)
        {
            return new PutItemRequest<EnumOption>(Dispatcher, $"enum_options/{enumOptionGid}").AddData(data);
        }
    }
}