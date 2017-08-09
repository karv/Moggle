using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	/// <summary>
	/// A component that listen for clicks in a rectangle.
	/// </summary>
	public abstract class ClickableControl : IGameComponent, IDisposable
	{
		bool _isInitialized;

		readonly MouseListener _mouseListener;

		/// <summary>
		/// The bounds of the listening area.
		/// </summary>
		public Rectangle Bounds;

		/// <param name="mouse">Mouse listener of the <see cref="Screens.IScreen"/></param>
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

		/// <summary>
		/// Initializes this object.
		/// </summary>
		protected virtual void Initialize()
		{
			_mouseListener.MouseClicked += Mouse_MouseClicked;
		}

		void Mouse_MouseClicked(object sender, MouseEventArgs e)
		{
			if (Bounds.Contains(e.Position))
				OnClick(e);
		}

		/// <summary>
		/// Invoked when the object is clicked.
		/// </summary>
		/// <param name="e">Mouse event args</param>
		protected abstract void OnClick(MouseEventArgs e);

		void IDisposable.Dispose()
		{
			Dispose();
		}
		/// <summary>
		/// Remove suscrptions.
		/// </summary>
		protected virtual void Dispose()
		{
			_mouseListener.MouseClicked -= Mouse_MouseClicked;
		}
	}
}