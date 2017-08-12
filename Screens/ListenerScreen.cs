using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Screens
{
	/// <summary>
	/// A <see cref="Screen"/> that has mouse and keyboard listeners
	/// </summary>
	public class ListenerScreen : Screen
	{
		/// <summary>
		/// The keyboard listener.
		/// </summary>
		public readonly KeyboardListener KeyboardListener;
		/// <summary>
		/// The mouse listener.
		/// </summary>
		public readonly MouseListener MouseListener;
		/// <summary>
		/// The listener component
		/// </summary>
		public readonly InputListenerComponent ListenerComponent;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Moggle.Screens.ListenerScreen"/> class.
		/// </summary>
		/// <param name="game">The Game</param>
		/// <param name="mouseSettings">Mouse settings. <c>null</c> for default.</param>
		/// <param name="keySettings">Keyboard settings. <c>null</c> for default.</param>
		public ListenerScreen(Game game,
									 MouseListenerSettings mouseSettings = null,
									 KeyboardListenerSettings keySettings = null)
			: base(game)
		{
			MouseListener = mouseSettings == null ? new MouseListener() : new MouseListener(mouseSettings);
			KeyboardListener = keySettings == null ? new KeyboardListener() : new KeyboardListener(keySettings);
			ListenerComponent = new InputListenerComponent(game, MouseListener, KeyboardListener);

			Components.Add(ListenerComponent);
		}
	}
}