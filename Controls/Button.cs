using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	public class Button : ClickableControl, IDrawable
	{
		readonly MouseListener _mouseListener;
		readonly Texture2D _texture;

		public Button(MouseListener mouse, Texture2D texture) : base(mouse)
		{
			_texture = texture;
		}

		protected override void OnClick(MouseEventArgs e) => Clicked?.Invoke(this, e);

		public void Draw(SpriteBatch batch)
		{
			batch.Draw(_texture, Bounds, Color.White);
		}

		public event EventHandler Clicked;
	}
}