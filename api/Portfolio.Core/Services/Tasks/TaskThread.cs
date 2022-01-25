using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Infrastructure;
using Portfolio.Core.Services.Settings;
using Portfolio.Domain.Dtos.ScheduleTasks;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Portfolio.Core.Services.Tasks;

public class TaskThread : IDisposable
{
    #region Fields

    private static readonly string _scheduleTaskUrl;
    private static readonly int? _timeout;

    private readonly Dictionary<string, string> _tasks;
    private Timer _timer;
    private bool _disposed;

    #endregion

    #region Ctor

    static TaskThread()
    {
        _scheduleTaskUrl = $"{EngineContext.Current.Resolve<ISettingService<ApiSettings>>().GetAsync().Result.ApiUrl.TrimEnd('/')}/scheduletask/runtask";
        _timeout = 30;
    }

    internal TaskThread()
    {
        _tasks = new Dictionary<string, string>();
        Seconds = 10 * 60;
    }

    #endregion

    #region Utilities

    private async System.Threading.Tasks.Task RunAsync()
    {
        if (Seconds <= 0)
            return;

        StartedUtc = DateTime.UtcNow;
        IsRunning = true;
        HttpClient client = null;

        foreach (var taskName in _tasks.Keys)
        {
            var taskType = _tasks[taskName];
            try
            {
                //create and configure client
                client = EngineContext.Current.Resolve<IHttpClientFactory>().CreateClient("ScheduleTask");
                if (_timeout.HasValue)
                    client.Timeout = TimeSpan.FromSeconds(_timeout.Value);

                //send post data
                var dto = new RunScheduleTaskDto
                {
                    TaskType = taskType
                };

                var json = JsonSerializer.Serialize(dto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(_scheduleTaskUrl, data);
            }
            catch (Exception ex)
            {
                var serviceScopeFactory = EngineContext.Current.Resolve<IServiceScopeFactory>();
                using var scope = serviceScopeFactory.CreateScope();
                // Resolve
                var logger = EngineContext.Current.Resolve<ILogger>(scope);

                logger.LogError("Error in shceduled task", ex);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
        }

        IsRunning = false;
    }

    private void TimerHandler(object state)
    {
        try
        {
            _timer.Change(-1, -1);

            RunAsync().Wait();
        }
        catch
        {
            // ignore
        }
        finally
        {
            if (RunOnlyOnce)
                Dispose();
            else
                _timer.Change(Interval, Interval);
        }
    }

    #endregion

    #region Methods

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            lock (this)
                _timer?.Dispose();

        _disposed = true;
    }

    public void InitTimer()
    {
        if (_timer == null)
            _timer = new Timer(TimerHandler, null, InitInterval, Interval);
    }

    public void AddTask(ScheduleTask task)
    {
        if (!_tasks.ContainsKey(task.Name))
            _tasks.Add(task.Name, task.Type);
    }

    #endregion

    #region Properties

    public int Seconds { get; set; }

    public int InitSeconds { get; set; }

    public DateTime StartedUtc { get; private set; }

    public bool IsRunning { get; private set; }

    public int Interval
    {
        get
        {
            //if somebody entered more than "2147483" seconds, then an exception could be thrown (exceeds int.MaxValue)
            var interval = Seconds * 1000;
            if (interval <= 0)
                interval = int.MaxValue;
            return interval;
        }
    }

    public int InitInterval
    {
        get
        {
            //if somebody entered less than "0" seconds, then an exception could be thrown
            var interval = InitSeconds * 1000;
            if (interval <= 0)
                interval = 0;
            return interval;
        }
    }

    public bool RunOnlyOnce { get; set; }

    #endregion
}