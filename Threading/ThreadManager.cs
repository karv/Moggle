using System;
using System.Collections.Generic;
using System.Linq;

namespace Moggle.Threading
{
	/// <summary>
	/// Manages the collection of threads in a <see cref="Game"/>.
	/// </summary>
	public class ThreadManager
	{
		/// <summary>
		/// Gets the actve thread.
		/// </summary>
		public ScreenThread ActiveThread { get; private set; }
		readonly HashSet<ScreenThread> _managedThreads;
		Game _game;

		/// <summary>
		/// Gets a collection of the managed threads.
		/// </summary>
		public IReadOnlyCollection<ScreenThread> ManagedThreads => _managedThreads.ToArray();

		internal ThreadManager(Game game)
		{
			_game = game;
			_managedThreads = new HashSet<ScreenThread>();
			CreateAndSwitch();
		}

		/// <summary>
		/// Creates and returns a new <see cref="ScreenThread"/>
		/// </summary>
		public ScreenThread Create()
		{
			var ret = new ScreenThread(_game);
			_managedThreads.Add(ret);
			return ret;
		}

		/// <summary>
		/// Creates, switches and returns a new <see cref="ScreenThread"/>
		/// </summary>
		/// <returns>The and switch.</returns>
		public ScreenThread CreateAndSwitch()
		{
			var ret = new ScreenThread(_game);
			_managedThreads.Add(ret);
			ActiveThread = ret;
			return ret;
		}

		/// <summary>
		/// Sets a managed thread as active
		/// </summary>
		/// <param name="thread">Thread</param>
		public void Switch(ScreenThread thread)
		{
			// Since there is no public ctor, there is no need to check whether the specified thread is managed.
			if (thread._disposed) throw new ObjectDisposedException("Cannot switch to disposed thread.");
			ActiveThread = thread;
		}

		/// <summary>
		/// Destroys the specified thread by invoking every disposable screen's Dispose and marking the thread
		/// as unmanaged.
		/// </summary>
		/// <param name="thread">The thread that will be destroyed.</param>
		public void Destroy(ScreenThread thread)
		{
			if (thread == ActiveThread) throw new InvalidOperationException("Cannot destroy active thread.");
			if (thread._disposed) return;
			thread.Dispose();
			_managedThreads.Remove(thread);
		}

		internal void Dispose()
		{
			foreach (var thread in _managedThreads) Destroy(thread);
		}
	}
}