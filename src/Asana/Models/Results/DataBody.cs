namespace Asana.Models.Results
{
    internal sealed class DataBody<TData> where TData : IData
    {
        public TData Data { get; }

        public DataBody(TData data)
        {
            Data = data;
        }
    }
}