using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class CustomField : AsanaResource
    {
        [JsonProperty("resource_subtype")]
        public string ResourceSubType { get; }
        [JsonProperty("is_global_to_workspace")]
        public bool IsGlobalToWorkspace { get; }
        [JsonProperty("enum_options")]
        public EnumOption[] EnumOptions { get; }
        [JsonProperty("has_notifications_enabled")]
        public bool HasNotificationsEnabled { get; }
        public string Name { get; }
        [JsonProperty("created_by")]
        public User CreatedBy { get; }
        [JsonProperty("currency_code")]
        public string? CurrencyCode { get; }
        [JsonProperty("custom_label")]
        public string? CustomLabel { get; }
        [JsonProperty("custom_label_position")]
        public string CustomLabelPosition { get; }
        public string Description { get; }
        public bool Enabled { get; }
        public string Format { get; }
        [JsonProperty("number_value")]
        public decimal? NumberValue { get; }
        public int Precision { get; }
        [JsonProperty("text_value")]
        public string? TextValue { get; }
        public string Type { get; }
        [JsonProperty("enum_value")]
        public EnumOption EnumValue { get; }

        internal CustomField(
            string gid,
            string resourceType,
            string resourceSubType,
            bool isGlobalToWorkspace,
            EnumOption[] enumOptions,
            bool hasNotificationsEnabled,
            string name,
            User createdBy,
            string? currencyCode,
            string? customLabel,
            string customLabelPosition,
            string description,
            bool enabled,
            string format,
            decimal? numberValue,
            int precision,
            string? textValue,
            string type,
            EnumOption enumValue) : base(gid, resourceType)
        {
            ResourceSubType = resourceSubType;
            IsGlobalToWorkspace = isGlobalToWorkspace;
            EnumOptions = enumOptions;
            HasNotificationsEnabled = hasNotificationsEnabled;
            Name = name;
            CreatedBy = createdBy;
            CurrencyCode = currencyCode;
            CustomLabel = customLabel;
            CustomLabelPosition = customLabelPosition;
            Description = description;
            Enabled = enabled;
            Format = format;
            NumberValue = numberValue;
            Precision = precision;
            TextValue = textValue;
            Type = type;
            EnumValue = enumValue;
        }
    }
}