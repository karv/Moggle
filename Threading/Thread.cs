using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Moggle.Threading
{
	public class Thread
	{
		readonly List<ScreenCallStatus> _screens;

		public Thread ()
		{
			_screens = new List<ScreenCallStatus> ();
		}

		public void UpdateRecursively (GameTime time)
		{
			for (int i = _screens.Count - 1; i >= 0; i--)
			{
				_screens [i].Screen.Update (time);
				if (!_screens [i].Iter.HasFlag (ThreadIterationMethod.UpdateBase)) return;
			}
		}

		public void DrawRecursively (GameTime time)
		{
			for (int i = _screens.Count - 1; i >= 0; i--)
			{
				_screens [i].Screen.Draw ();
				if (!_screens [i].Iter.HasFlag (ThreadIterationMethod.DrawBase)) return;
			}
		}

		public void Stack (IScreen scr)
		{
			Stack (scr, new ThreadIterationMethod ());
		}

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
