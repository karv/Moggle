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

		public IReadOnlyCollection<ScreenThread> ManagedThreads => _managedThreads.ToArray();

		internal ThreadManager(Game game)
		{
			_game = game;
			_managedThreads = new HashSet<ScreenThread>();
			CreateAndSwitch();
		}

		public ScreenThread Create()
		{
			var ret = new ScreenThread(_game);
			_managedThreads.Add(ret);
			return ret;
		}

		public ScreenThread CreateAndSwitch()
		{
			var ret = new ScreenThread(_game);
			_managedThreads.Add(ret);
			ActiveThread = ret;
			return ret;
		}

		public void Switch(ScreenThread thread)
		{
			if (thread._disposed) throw new ObjectDisposedException("Cannot switch to disposed thread.");
			ActiveThread = thread;
		}

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