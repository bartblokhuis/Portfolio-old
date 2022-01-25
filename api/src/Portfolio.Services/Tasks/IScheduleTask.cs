using System.Threading.Tasks;

namespace Portfolio.Services.Tasks;

public interface IScheduleTask
{
    Task ExecuteAsync();
}
