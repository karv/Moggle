
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

		internal ThreadManager (Game game)
		{
			ActiveThread = new ScreenThread (game);
		}
	}
}