using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Moggle.Screens;

namespace Moggle.Threading
{
	/// <summary>
	/// A screen stack of invocations.
	/// </summary>
	public class ScreenThread
	{
		readonly List<ScreenCallStatus> _screens;
		/// <summary>
		/// The color of the background.
		/// </summary>
		public Color BackgroundColor = Color.Black;
		/// <summary>
		/// The game class.
		/// </summary>
		public readonly Game Game;
		/// <summary>
		/// Gets the top most screen on the stack.
		/// </summary>
		public IScreen TopmostScreen => _screens[_screens.Count - 1].Screen;
		/// <summary>
		/// Gets the ordered invocation list.
		/// </summary>
		public IReadOnlyList<IScreen> Screens => _screens.Select(z => z.Screen).Reverse().ToArray();

		internal bool _disposed { get; private set; }
		/// <summary>
		/// </summary>
		internal ScreenThread(Game game)
		{
			_screens = new List<ScreenCallStatus>();
			Game = game;
		}

		/// <summary>
		/// Updates the screens in the proper order.
		/// </summary>
		public void UpdateRecursively(GameTime time)
		{
			for (int i = _screens.Count - 1; i >= 0; i--)
			{
				var updateBelow = _screens[i].Iter.HasFlag(ThreadIterationMethod.UpdateBase);
				_screens[i].Screen.Update(time);
				if (!updateBelow) return;
			}
		}

		/// <summary>
		/// Draws the screens in the proper order.
		/// </summary>
		public void DrawRecursively()
		{
			Game.GraphicsDevice.Clear(BackgroundColor);
			for (int i = _screens.Count - 1; i >= 0; i--)
			{
				_screens[i].Screen.Draw();
				if (!_screens[i].Iter.HasFlag(ThreadIterationMethod.DrawBase)) return;
			}
		}

		/// <summary>
		/// Initializes and stacks a specified screen into this thread ignoring the bottom screens.
		/// </summary>
		public void Stack(IScreen scr)
		{
			if (scr == null) throw new ArgumentNullException(nameof(scr));
			scr.Initialize();
			Stack(scr, ThreadIterationMethod.IgnoreBottom);
		}

		/// <summary>
		/// Initializes and stack a specified screen with the specified options.
		/// </summary>
		/// <param name="scr">Screen</param>
		/// <param name="iterMethod">Iteration method</param>
		public void Stack(IScreen scr, ThreadIterationMethod iterMethod)
		{
			if (scr == null) throw new ArgumentNullException(nameof(scr));
			scr.Initialize();
			_screens.Add(new ScreenCallStatus { Screen = scr, Iter = iterMethod });
		}

		/// <summary>
		/// Terminates the topmost screen and returns to the next screen in the stack.
		/// </summary>
		public void TerminateLast()
		{
			_screens.RemoveAt(_screens.Count - 1);
		}

		internal void Dispose()
		{
			foreach (var c in _screens.Select(z => z.Screen).OfType<IDisposable>()) c.Dispose();
			_disposed = true;
		}

		class ScreenCallStatus
		{
			public IScreen Screen;
			public ThreadIterationMethod Iter;
		}
	}
}