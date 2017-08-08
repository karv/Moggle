using Microsoft.Xna.Framework;

namespace Moggle
{
	public interface IScreen
	{
		void Update (GameTime gameTime);
		void Draw ();
	}
}
