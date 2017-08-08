using Microsoft.Xna.Framework;

namespace Moggle.Screens
{
	/// <summary>
	/// A screen that draws and updates
	/// </summary>
	public interface IScreen
	{
		/// <summary>
		/// Updates this screen and its components.
		/// </summary>
		void Update (GameTime gameTime);
		/// <summary>
		/// Draws this screen and its components.
		/// </summary>
		void Draw ();
	}
}