using Asana.Dispatchers;

namespace Asana.Resources
{
    public abstract class Resource
    {
        protected Dispatcher Dispatcher { get; }

        protected Resource(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }
    }
}