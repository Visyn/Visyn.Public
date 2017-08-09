

namespace Visyn.Public.Log
{
    public interface IEventLevel<T>
    {
        T EventLevel { get; }
    }
}