using Newtonsoft.Json;

namespace Asana.Models.Results
{
    public sealed class Error
    {
        public string Message { get; }
        public string Phrase { get; }
        public string Help { get; }

        [JsonConstructor]
        internal Error(string message, string help, string phrase)
        {
            Message = message;
            Phrase = phrase;
            Help = help;
        }
    }
}