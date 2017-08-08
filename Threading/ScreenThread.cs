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

		public Color BackgroundColor = Color.Black;
		public readonly Game Game;

		/// <summary>
		/// Gets the ordered invocation list.
		/// </summary>
		public IReadOnlyList<Screens.IScreen> Screens => _screens.Select (z => z.Screen).Reverse ().ToArray ();

		/// <summary>
		/// </summary>
		internal ScreenThread (Game game)
		{
			_screens = new List<ScreenCallStatus> ();
			Game = game;
		}

		/// <summary>
		/// Updates the screens in the proper order.
		/// </summary>
		public void UpdateRecursively (GameTime time)
		{
			for (int i = _screens.Count - 1; i >= 0; i--)
			{
				_screens [i].Screen.Update (time);
				if (!_screens [i].Iter.HasFlag (ThreadIterationMethod.UpdateBase)) return;
			}
		}

		/// <summary>
		/// Draws the screens in the proper order.
		/// </summary>
		public void DrawRecursively ()
		{
			Game.GraphicsDevice.Clear (BackgroundColor);
			for (int i = _screens.Count - 1; i >= 0; i--)
			{
				_screens [i].Screen.Draw ();
				if (!_screens [i].Iter.HasFlag (ThreadIterationMethod.DrawBase)) return;
			}
		}

		/// <summary>
		/// Stacks a new screen
		/// </summary>
		public void Stack (IScreen scr)
		{
			Stack (scr, new ThreadIterationMethod ());
		}

		/// <summary>
		/// Stack a new screen with the specified options
		/// </summary>
		/// <param name="scr">Screen</param>
		/// <param name="iterMethod">Iteration method</param>
		public void Stack (IScreen scr, ThreadIterationMethod iterMethod)
		{
			if (scr == null) throw new System.ArgumentNullException (nameof (scr));
			_screens.Add (new ScreenCallStatus { Screen = scr, Iter = iterMethod });
		}

		class ScreenCallStatus
		{
			public IScreen Screen;
			public ThreadIterationMethod Iter;
		}
	}
}