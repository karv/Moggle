using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	/// <summary>
	/// Control mouse event arguments.
	/// </summary>
	public class ControlMouseEventArgs
	{
		/// <summary>
		/// Relative position to the control.
		/// </summary>
		public Point RelativePosition;
		/// <summary>
		/// Mouse arguments.
		/// </summary>
		public MouseEventArgs MouseArgs;
	}
}