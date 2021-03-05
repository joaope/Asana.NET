namespace Asana.Models
{
    public sealed class EmptyData : IData
    {
        internal EmptyData() {}

        public static EmptyData Instance { get; } = new EmptyData();
    }
}