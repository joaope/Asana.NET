using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Asana.Requests
{
    public abstract class Request
    {
        protected Dispatcher Dispatcher { get; }
        protected string RequestPath { get; }
        protected HttpContent? Content { get; private set; }

        private readonly NameValueCollection _query = new NameValueCollection();
        private readonly ISet<string> _fields = new HashSet<string>();

        protected string RequestUrl
        {
            get
            {
                var sb = new StringBuilder(RequestPath);
                var addQuery = false;

                if (_fields.Count > 0 && _query.AllKeys.All(key => key != "opt_fields"))
                {
                    AddQueryParameter("opt_fields", $"{string.Join(",", _fields)}");
                }

                foreach (string key in _query.Keys)
                {
                    if (string.IsNullOrWhiteSpace(key))
                    {
                        continue;
                    }

                    string[] values = _query.GetValues(key) ?? new string[0];

                    foreach (var value in values)
                    {
                        sb.Append(!addQuery ? "/?" : "&");
                        addQuery = true;

                        if (string.IsNullOrEmpty(value))
                        {
                            sb.Append(Uri.EscapeUriString(key));
                        }
                        else
                        {
                            sb.AppendFormat("{0}={1}", Uri.EscapeUriString(key), Uri.EscapeDataString(value));
                        }
                    }
                }

                return sb.ToString();
            }
        }

        protected Request(Dispatcher dispatcher, string requestPath)
        {
            Dispatcher = dispatcher;
            RequestPath = requestPath;
        }

        public Request AddData(object data)
        {
            AddJsonContent(new {data});
            return this;
        }

        public Request AddField(string fieldName)
        {
            _fields.Add(fieldName);
            return this;
        }

        public Request AddFields(params string[] fieldNames)
        {
            foreach (var fieldName in fieldNames)
            {
                AddField(fieldName);
            }

            return this;
        }

        public Request AddFields(IEnumerable<string> fieldNames)
        {
            AddFields(fieldNames?.ToArray() ?? new string[0]);
            return this;
        }

        public Request AddQueryParameter(string key, string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return this;
            }

            _query.Add(key, value);
            return this;
        }

        public Request PrettyOutput(bool pretty)
        {
            if (pretty)
            {
                AddQueryParameter("opt_pretty", string.Empty);
            }
            else
            {
                RemoveQueryParameter("opt_pretty");
            }

            return this;
        }

        internal Request RemoveQueryParameter(string key)
        {
            _query.Remove(key);
            return this;
        }

        internal Request AddJsonContent(object contentObj)
        {
            Content = new StringContent(
                JsonConvert.SerializeObject(contentObj), null, "application/json");
            return this;
        }

        internal Request AddFile(byte[] fileData, string fileName)
        {
            var part = new ByteArrayContent(fileData, 0, fileData.Length);
            part.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                FileName = @$"""{fileName}""",
                Name = @"""file"""
            };

            var multiPartFormContent = new MultipartFormDataContent(Guid.NewGuid().ToString())
            {
                {part, @"""file""", @$"""{fileName}"""}
            };

            Content = multiPartFormContent;

            return this;
        }
    }
}
