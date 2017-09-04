using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Threading;
using System.Threading.Tasks;
using TwinRx.Interfaces.Enums;

namespace TwinRx.Interfaces
{
    public interface ITwinRxClient
    {
        IObservable<T> ObservableFor<T>(string variableName, TransmissionMode mode, TimeSpan updateRate);
        IDisposable StreamTo<T>(string variableName, IObservable<T> observable, IScheduler scheduler = null);
        Task WriteAsync<T>(string variableName, T value, CancellationToken token);
        IDisposable Write<T>(string variableName, T value, IScheduler scheduler = null);
    }
}