using System.Threading.Tasks;

namespace Portfolio.Core.Services.Tasks;

public interface IScheduleTask
{
    Task ExecuteAsync();
}
