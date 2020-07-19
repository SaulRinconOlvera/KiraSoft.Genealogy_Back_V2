using Hangfire;
using Hangfire.Annotations;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KiraSoft.CrossCutting.Jobs
{
    public class ApplicationJobs
    {
        public static string Enqueue([InstantHandle][NotNull] Expression<Action> methodCall) =>
            BackgroundJob.Enqueue(methodCall);

        public static string Enqueue<T>([InstantHandle][NotNull] Expression<Func<T, Task>> methodCall) =>
            BackgroundJob.Enqueue<T>(methodCall);

        public static string Enqueue<T>([InstantHandle][NotNull] Expression<Action<T>> methodCall) =>
            BackgroundJob.Enqueue<T>(methodCall);

        public static string Enqueue([InstantHandle][NotNull] Expression<Func<Task>> methodCall) =>
            BackgroundJob.Enqueue(methodCall);

        public static string Schedule<T>([InstantHandle][NotNull] Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt) =>
            BackgroundJob.Schedule<T>(methodCall, enqueueAt);

        public static string Schedule<T>([InstantHandle][NotNull] Expression<Action<T>> methodCall, DateTimeOffset enqueueAt) =>
            BackgroundJob.Schedule<T>(methodCall, enqueueAt);

        public static string Schedule<T>([InstantHandle][NotNull] Expression<Func<T, Task>> methodCall, TimeSpan delay) =>
            BackgroundJob.Schedule<T>(methodCall, delay);

        public static string Schedule<T>([InstantHandle][NotNull] Expression<Action<T>> methodCall, TimeSpan delay) =>
            BackgroundJob.Schedule<T>(methodCall, delay);

        public static string Schedule([InstantHandle][NotNull] Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt) =>
            BackgroundJob.Schedule(methodCall, enqueueAt);

        public static string Schedule([InstantHandle][NotNull] Expression<Action> methodCall, DateTimeOffset enqueueAt) =>
            BackgroundJob.Schedule(methodCall, enqueueAt);

        public static string Schedule([InstantHandle][NotNull] Expression<Func<Task>> methodCall, TimeSpan delay) =>
            BackgroundJob.Schedule(methodCall, delay);

        public static string Schedule([InstantHandle][NotNull] Expression<Action> methodCall, TimeSpan delay) =>
            BackgroundJob.Schedule(methodCall, delay);
    }
}
