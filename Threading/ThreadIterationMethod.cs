using System;

namespace Moggle.Threading
{
	/// <summary>
	/// Specifies how a <see cref="ScreenThread"/> uses lower <see cref="Screens.IScreen"/>.
	/// </summary>
	[Flags]
	public enum ThreadIterationMethod
	{
		/// <summary>
		/// Draw the next screen
		/// </summary>
		DrawBase = 0x0001,
		/// <summary>
		/// Updates the next screen
		/// </summary>
		UpdateBase = 0x0002,
		/// <summary>
		/// Draw and updates next screen.
		/// </summary>
		DrawAll = DrawBase | UpdateBase,
		/// <summary>
		/// All base screen are ignored.
		/// </summary>
		IgnoreBottom = 0x0000,
		/// <summary>
		/// Draw base, but do not update.
		/// </summary>
		Dialog = DrawBase
	}
}