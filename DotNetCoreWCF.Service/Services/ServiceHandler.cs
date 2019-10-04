using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Host.Services
{
	public static class ServiceHandler
	{
		public static T HandleExceptions<T>(Func<T> worker, int timeoutMS = 20000, string loggingHint = null, int frameLevel = 1, ILogger logger = null)
		{
			int duration = 0;
			Task<T> task = Task<T>.Run(worker);

			try
			{
				int waitAttempt = 0;
				System.Diagnostics.Stopwatch profiler = null;

				// Wait until the task is complete.  For now, don't do anything if it runs away, but we'll at least track down the runaways
				while (!task.Wait(timeoutMS))
				{
					LogTimeout(loggingHint, frameLevel, ref waitAttempt, ref profiler);

					++waitAttempt;

					duration += timeoutMS;
					if (duration >= (5 * 60000))
						throw new TimeoutException("Request timed out");
				}

				if (waitAttempt > 0)
					LogCompletion(timeoutMS, profiler);

				// Split it out for ability to debug result before it returns back from service
				T result = task.Result;

				return result;
			}
			catch (AggregateException aEx)
			{
				Exception ex = aEx.InnerExceptions.FirstOrDefault();

				if (ex == null)
					throw new FaultException(aEx.Message);

				// Log the error
				logger?.LogError(ex, ex.Message);
				// Throw a fault instead of an exception
				throw new FaultException(ex.Message);
			}
			catch (Exception ex)
			{
				// Log the error
				logger?.LogError(ex, ex.Message);
				// Throw a fault instead of an exception
				throw new FaultException(ex.Message);
			}
		}

		public static T HandleExceptions<T>(Func<T> worker, Func<string> loggingHintFunc, int timeoutMS = 20000, int frameLevel = 1, ILogger logger = null)
		{
			int duration = 0;
			Task<T> task = Task<T>.Run(worker);

			try
			{
				int waitAttempt = 0;
				System.Diagnostics.Stopwatch profiler = null;

				// Wait until the task is complete.  For now, don't do anything if it runs away, but we'll at least track down the runaways
				while (!task.Wait(timeoutMS))
				{
					LogTimeout(loggingHintFunc(), frameLevel, ref waitAttempt, ref profiler);

					++waitAttempt;

					duration += timeoutMS;
					if (duration >= (5 * 60000))
						throw new TimeoutException("Request timed out");
				}

				if (waitAttempt > 0)
					LogCompletion(timeoutMS, profiler);

				// Split it out for ability to debug result before it returns back from service
				T result = task.Result;

				return result;
			}
			catch (AggregateException aEx)
			{
				Exception ex = aEx.InnerExceptions.FirstOrDefault();

				if (ex == null)
					throw new FaultException(aEx.Message);

				logger?.LogError(ex, ex.Message);

				// Throw a fault instead of an exception
				throw new FaultException(ex.Message);
			}
			catch (Exception ex)
			{
				logger?.LogError(ex, ex.Message);

				// Throw a fault instead of an exception
				throw new FaultException(ex.Message);
			}
		}

		public static void HandleExceptions(Action worker, int timeoutMS = 20000, string loggingHint = null, int frameLevel = 1, ILogger logger = null)
		{
			int duration = 0;
			Task task = Task.Run(worker);

			try
			{
				int waitAttempt = 0;
				System.Diagnostics.Stopwatch profiler = null;

				// Wait until the task is complete.  For now, don't do anything if it runs away, but we'll at least track down the runaways
				while (!task.Wait(timeoutMS))
				{
					LogTimeout(loggingHint, frameLevel, ref waitAttempt, ref profiler);

					++waitAttempt;

					duration += timeoutMS;
					if (duration >= (5 * 60000))
						throw new TimeoutException("Request timed out");
				}

				if (waitAttempt > 0)
					LogCompletion(timeoutMS, profiler);
			}
			catch (AggregateException aEx)
			{
				Exception ex = aEx.InnerExceptions.FirstOrDefault();

				if (ex == null)
					return;

				// Log the error
				logger?.LogError(ex, ex.Message);
				// Throw a fault instead of an exception
				throw new FaultException(ex.Message);
			}
			catch (Exception ex)
			{
				// Log the error
				logger?.LogError(ex, ex.Message);
				// Throw a fault instead of an exception
				throw new FaultException(ex.Message);
			}
		}

		private static void LogTimeout(string loggingHint, int frameLevel, ref int waitAttempt, ref System.Diagnostics.Stopwatch profiler, ILogger logger = null)
		{
			if (profiler == null)
			{
				profiler = new System.Diagnostics.Stopwatch();
				profiler.Start();
			}

			StringBuilder builder = new StringBuilder();

			builder.AppendLine(string.Format("Thread {0}: Request timeout #{1}", Thread.CurrentThread.ManagedThreadId, ++waitAttempt));

			// On the first timeout, log the stack trace
			if (waitAttempt == 1)
			{
				StackTrace stackTrace = new StackTrace();           // get call stack
				StackFrame[] stackFrames = stackTrace.GetFrames();

				// The frameLevel is going to be one deeper than whatever it was coming in. So skip frameLevel + 1 to get the originating method call
				builder.AppendFormat("*** Call from {0}", stackFrames.Skip(frameLevel + 1).First().GetMethod().Name);
				if (!string.IsNullOrEmpty(loggingHint))
					builder.AppendLine(string.Format(". Caller hint: {0}", loggingHint));
				else
					builder.AppendLine();
			}

			logger?.LogInformation(builder.ToString());
		}

		private static void LogCompletion(int timeoutMS, System.Diagnostics.Stopwatch profiler, ILogger logger = null)
		{
			long durationMS = (long)timeoutMS;

			if (profiler != null)
			{
				profiler.Stop();
				durationMS += profiler.ElapsedMilliseconds;
			}

			logger?.LogInformation(string.Format("Thread {0}: Managed to complete after {1}ms",
				Thread.CurrentThread.ManagedThreadId, durationMS));
		}
	}
}
