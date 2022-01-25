using Portfolio.Core.Caching;
using Portfolio.Core.Infrastructure;
using Portfolio.Domain.Models;

namespace Portfolio.Services.Tasks;

public partial class PortfolioTask
{
    #region Fields

    private bool? _enabled;

    #endregion

    #region Ctor

    public PortfolioTask(ScheduleTask task)
    {
        ScheduleTask = task;
    }

    #endregion

    #region Utilities

    private void ExecuteTask()
    {
        var scheduleTaskService = EngineContext.Current.Resolve<IScheduleTaskService>();

        if (!Enabled)
            return;

        var type = Type.GetType(ScheduleTask.Type) ??
            //ensure that it works fine when only the type name is specified (do not require fully qualified names)
            AppDomain.CurrentDomain.GetAssemblies()
            .Select(a => a.GetType(ScheduleTask.Type))
            .FirstOrDefault(t => t != null);
        if (type == null)
            throw new Exception($"Schedule task ({ScheduleTask.Type}) cannot by instantiated");

        object instance = null;
        try
        {
            instance = EngineContext.Current.Resolve(type);
        }
        catch
        {
            //try resolve
        }

        if (instance == null)
            //not resolved
            instance = EngineContext.Current.ResolveUnregistered(type);

        if (instance is not IScheduleTask task)
            return;

        ScheduleTask.LastStartUtc = DateTime.UtcNow;
        //update appropriate datetime properties
        scheduleTaskService.UpdateTaskAsync(ScheduleTask).Wait();
        task.ExecuteAsync().Wait();
        ScheduleTask.LastEndUtc = ScheduleTask.LastSuccessUtc = DateTime.UtcNow;
        //update appropriate datetime properties
        scheduleTaskService.UpdateTaskAsync(ScheduleTask).Wait();
    }

    protected virtual bool IsTaskAlreadyRunning(ScheduleTask scheduleTask)
    {
        //task run for the first time
        if (!scheduleTask.LastStartUtc.HasValue && !scheduleTask.LastEndUtc.HasValue)
            return false;

        var lastStartUtc = scheduleTask.LastStartUtc ?? DateTime.UtcNow;

        //task already finished
        if (scheduleTask.LastEndUtc.HasValue && lastStartUtc < scheduleTask.LastEndUtc)
            return false;

        //task wasn't finished last time
        if (lastStartUtc.AddSeconds(scheduleTask.Seconds) <= DateTime.UtcNow)
            return false;

        return true;
    }

    #endregion

    #region Methods

    public async Task ExecuteAsync(bool throwException = false, bool ensureRunOncePerPeriod = true)
    {
        if (ScheduleTask == null || !Enabled)
            return;

        if (ensureRunOncePerPeriod && IsTaskAlreadyRunning(ScheduleTask))
            return;

        try
        {
            //get expiration time
            var expirationInSeconds = Math.Min(ScheduleTask.Seconds, 300) - 1;
            var expiration = TimeSpan.FromSeconds(expirationInSeconds);

            //execute task with lock
            var locker = EngineContext.Current.Resolve<ILocker>();
            locker.PerformActionWithLock(ScheduleTask.Type, expiration, ExecuteTask);
        }
        catch (Exception exc)
        {
            var scheduleTaskService = EngineContext.Current.Resolve<IScheduleTaskService>();

            ScheduleTask.Enabled = !ScheduleTask.StopOnError;
            ScheduleTask.LastEndUtc = DateTime.UtcNow;
            await scheduleTaskService.UpdateTaskAsync(ScheduleTask);

            if (throwException)
                throw;
        }
    }

    #endregion

    #region Properties

    public ScheduleTask ScheduleTask { get; }

    public bool Enabled
    {
        get
        {
            if (!_enabled.HasValue)
                _enabled = ScheduleTask?.Enabled;

            return _enabled.HasValue && _enabled.Value;
        }

        set => _enabled = value;
    }

    #endregion
}
