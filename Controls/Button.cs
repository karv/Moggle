using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	public class Button : IGameComponent, IDrawable
	{
		bool _isInitialized;

		MouseListener _mouseListener;

		public Button(MouseListener mouse)
		{
			_mouseListener = mouse ?? throw new ArgumentNullException(nameof(mouse));
		}

		public Rectangle Bounds;


		void IGameComponent.Initialize()
		{
			if (_isInitialized) return;

			_isInitialized = true;
			_mouseListener.MouseClicked += Mouse_MouseClicked;
		}

		void Mouse_MouseClicked(object sender, MouseEventArgs e)
		{
			Clicked?.Invoke(this, e);
		}

		public event EventHandler Clicked;
	}
}
