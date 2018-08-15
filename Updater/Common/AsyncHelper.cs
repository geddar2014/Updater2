// Decompiled with JetBrains decompiler
// Type: Abp.Threading.AsyncHelper
// Assembly: Abp, Version=3.7.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 4EB87A06-6E7D-4BFD-ACAC-512616C81FAF
// Assembly location: C:\Users\gedda\.nuget\packages\abp\3.7.1\lib\netstandard2.0\Abp.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Updater.Common
{
	/// <summary>
	///     Provides some helper methods to work with async methods.
	/// </summary>
	public static class AsyncHelper
	{
		/// <summary>Checks if given method is an async method.</summary>
		/// <param name="method">A method to check</param>
		public static bool IsAsync(this MethodInfo method)
		{
			if (method.ReturnType == typeof(Task))
				return true;
			if (method.ReturnType.GetTypeInfo().IsGenericType)
				return method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
			return false;
		}

		/// <summary>Checks if given method is an async method.</summary>
		/// <param name="method">A method to check</param>
		[Obsolete("Use MethodInfo.IsAsync() extension method!")]
		public static bool IsAsyncMethod(MethodInfo method)
		{
			return method.IsAsync();
		}

		/// <summary>Runs a async method synchronously.</summary>
		/// <param name="func">A function that returns a result</param>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <returns>Result of the async operation</returns>
		public static TResult RunSync<TResult>(Func<Task<TResult>> func)
		{
			return AsyncContext.Run(func);
		}

		/// <summary>Runs a async method synchronously.</summary>
		/// <param name="action">An async action</param>
		public static void RunSync(Func<Task> action)
		{
			AsyncContext.Run(action);
		}

		public static async Task RunWithMaxDegreeOfConcurrency<T>(this IEnumerable<T> collection,
		                                                          int maxDegreeOfConcurrency, Func<T, Task> taskFactory)
		{
			var activeTasks = new List<Task>(maxDegreeOfConcurrency);
			foreach (var task in collection.Select(taskFactory))
			{
				activeTasks.Add(task);
				if (activeTasks.Count == maxDegreeOfConcurrency)
				{
					await Task.WhenAny(activeTasks.ToArray());
					//observe exceptions here
					activeTasks.RemoveAll(t => t.IsCompleted);
				}
			}

			await Task.WhenAll(activeTasks.ToArray()).ContinueWith(t =>
			{
				//observe exceptions in a manner consistent with the above   
			});
		}

		public static Task ForEachAsync<T>(
				this IEnumerable<T> source, int dop, Func<T, Task> body)
		{
			return Task.WhenAll(
					from partition in Partitioner.Create(source).GetPartitions(dop)
					select Task.Run(async delegate
					{
						using (partition)
						{
							while (partition.MoveNext())
								await body(partition.Current).ContinueWith(t =>
								{
									//observe exceptions
								});
						}
					}));
		}
	}
}