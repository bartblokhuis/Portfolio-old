using Portfolio.Core.Infrastructure;
using System.Collections.ObjectModel;

namespace Portfolio.Services.Tasks;

public class TaskManager
{
    #region Fields

    private readonly List<TaskThread> _taskThreads = new List<TaskThread>();

    #endregion

    #region Ctor

    private TaskManager()
    {
    }

    #endregion

    #region Methods

    public void Initialize()
    {
        _taskThreads.Clear();

        var taskService = EngineContext.Current.Resolve<IScheduleTaskService>();

        var scheduleTasks = taskService
            .GetAllTasksAsync().Result
            .OrderBy(x => x.Seconds)
            .ToList();

        foreach (var scheduleTask in scheduleTasks)
        {
            var taskThread = new TaskThread
            {
                Seconds = scheduleTask.Seconds
            };

            //sometimes a task period could be set to several hours (or even days)
            //in this case a probability that it'll be run is quite small (an application could be restarted)
            //calculate time before start an interrupted task
            if (scheduleTask.LastStartUtc.HasValue)
            {
                //seconds left since the last start
                var secondsLeft = (DateTime.UtcNow - scheduleTask.LastStartUtc).Value.TotalSeconds;

                if (secondsLeft >= scheduleTask.Seconds)
                    //run now (immediately)
                    taskThread.InitSeconds = 0;
                else
                    //calculate start time
                    //and round it (so "ensureRunOncePerPeriod" parameter was fine)
                    taskThread.InitSeconds = (int)(scheduleTask.Seconds - secondsLeft) + 1;
            }
            else
            {
                //first start of a task
                taskThread.InitSeconds = scheduleTask.Seconds;
            }

            taskThread.AddTask(scheduleTask);
            _taskThreads.Add(taskThread);
        }
    }

    public void Start()
    {
        foreach (var taskThread in _taskThreads)
        {
            taskThread.InitTimer();
        }
    }

    public void Stop()
    {
        foreach (var taskThread in _taskThreads)
        {
            taskThread.Dispose();
        }
    }

    #endregion

    #region Properties

    public static TaskManager Instance { get; } = new TaskManager();

    public IList<TaskThread> TaskThreads => new ReadOnlyCollection<TaskThread>(_taskThreads);

    #endregion
}
