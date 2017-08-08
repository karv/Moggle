using System;

namespace Moggle.Threading
{
	/// <summary>
	/// Specifies how a <see cref="ScreenThread"/> uses lower <see cref="IScreen"/>.
	/// </summary>
	[Flags]
	public enum ThreadIterationMethod
	{
		/// <summary>
		/// Draw the next screen
		/// </summary>
		DrawBase,
		/// <summary>
		/// Updates the next screen
		/// </summary>
		UpdateBase,
		/// <summary>
		/// Draw and updates next screen.
		/// </summary>
		DrawAll = DrawBase | UpdateBase,
		/// <summary>
		/// All base screen are ignored.
		/// </summary>
		Default = 0,
		/// <summary>
		/// Draw base, but do not update.
		/// </summary>
		Dialog = DrawBase
	}
}