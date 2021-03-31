namespace Asana
{
    public interface IConfigurableAsanaClient
    {
        AsanaClientOptions Options { get; }
        IAsanaClient WithDispatcher(Dispatcher dispatcher);
    }
}