using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Moggle.Screens
{
	/// <summary>
	/// A generic screen.
	/// </summary>
	public class Screen : ScreenBase
	{
		/// <summary>
		/// </summary>
		public Screen (Game game) : base (game) { }

		/// <param name="game">Game.</param>
		/// <param name="components">Components.</param>
		public Screen (Game game, IEnumerable<IGameComponent> components) : base (game)
		{
			foreach (var z in components)
				Components.Add (z);
		}
	}
}