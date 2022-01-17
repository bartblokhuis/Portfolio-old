using System;

namespace Portfolio.Core.Caching;

public interface ILocker
{
    bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action);
}