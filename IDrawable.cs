using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Moggle
{
	/// <summary>
	/// A drawable component
	/// </summary>
	public interface IDrawable : IGameComponent
	{
		/// <summary>
		/// Draw using the specified <see cref="SpriteBatch"/>.
		/// </summary>
		void Draw (SpriteBatch batch);
	}
}