namespace Asana.Models.Results
{
    public sealed class NextPageInformation
    {
        public string Offset { get; }

        public NextPageInformation(string offset)
        {
            Offset = offset;
        }
    }
}