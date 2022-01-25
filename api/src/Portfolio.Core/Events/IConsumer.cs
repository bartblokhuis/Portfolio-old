using System.Threading.Tasks;

namespace Portfolio.Core.Events;
public interface IConsumer<T>
{
    Task HandleEventAsync(T eventMessage);
}