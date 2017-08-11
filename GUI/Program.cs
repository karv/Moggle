using Moggle;

namespace GUI
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var game = new Game();
			game.Run();
			game.Dispose();
		}
	}
}