using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	public abstract class ClickableControl : IGameComponent, IDisposable
	{
		bool _isInitialized;

		MouseListener _mouseListener;

		protected ClickableControl(MouseListener mouse)
		{
			_mouseListener = mouse ?? throw new ArgumentNullException(nameof(mouse));
		}

		void IGameComponent.Initialize()
		{
			if (_isInitialized) return;

			_isInitialized = true;
			Initialize();
		}

		protected virtual void Initialize()
		{
			_mouseListener.MouseClicked += Mouse_MouseClicked;
		}

		void Mouse_MouseClicked(object sender, MouseEventArgs e) => OnClick(e);

		protected abstract void OnClick(MouseEventArgs e);

		void IDisposable.Dispose()
		{
			Dispose();
		}

		protected virtual void Dispose()
		{
			_mouseListener.MouseClicked -= Mouse_MouseClicked;
		}
	}
}