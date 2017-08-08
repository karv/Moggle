using Moggle.Threading;

namespace Moggle
{
	/// <summary>
	/// Game
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
		/// <summary>
		/// The screen threads manager for the game
		/// </summary>
		public readonly ThreadManager Threads;

		/// <summary>
		/// </summary>
		public Game ()
		{
			Threads = new ThreadManager (this);
		}
	}
}