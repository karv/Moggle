using Moggle.Threading;

namespace Moggle
{
	public class Game : Microsoft.Xna.Framework.Game
	{
		public readonly ThreadManager Threads;

		public Game ()
		{
			Threads = new ThreadManager ();
		}
	}
}
