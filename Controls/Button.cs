using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	/// <summary>
	/// A simple button-like control.
	/// </summary>
	public class Button : ClickableControl, IDrawable
	{
		readonly Texture2D _texture;

		/// <summary>
		/// The location and size.
		/// </summary>
		public Rectangle Location;

		/// <summary>
		/// The bounds of the listening area.
		/// </summary>
		protected override Rectangle Bounds => Location;

		/// <param name="mouse">Mouse listener of the <see cref="Screens.IScreen"/></param>
		/// <param name="texture">The texture for the button</param>
		public Button(MouseListener mouse, Texture2D texture) : base(mouse)
		{
			_texture = texture;
		}

		/// <summary>
		/// Invoked when the object is clicked.
		/// </summary>
		/// <param name="e">Mouse event args</param>
		protected override void OnClick(MouseEventArgs e) => Clicked?.Invoke(this, e);


		/// <summary>
		/// Draw the button.
		/// </summary>
		public void Draw(SpriteBatch batch)
		{
			batch.Draw(_texture, Bounds, Color.White);
		}

		/// <summary>
		/// Ocurrs when the button is clicked.
		/// </summary>
		public event EventHandler Clicked;
	}
}