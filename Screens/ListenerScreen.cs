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
		public KeyboardListener KeyboardListener { get; private set; }
		/// <summary>
		/// The mouse listener.
		/// </summary>
		public MouseListener MouseListener { get; private set; }
		/// <summary>
		/// The listener component
		/// </summary>
		public InputListenerComponent ListenerComponent { get; }

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
			ListenerComponent = new InputListenerComponent(game);

			Components.Add(ListenerComponent);
		}


		/// <summary>
		/// Construct the listeners.
		/// This method is controlled by the <see cref="P:Moggle.Screens.ScreenBase.IsInitialized" /> flag, so
		/// it will only be called once.
		/// </summary>
		protected override void DoInitialization()
		{
			MouseListener = new MouseListener(ScreenViewport);
			KeyboardListener = new KeyboardListener();
			ListenerComponent.Listeners.Add(MouseListener);
			ListenerComponent.Listeners.Add(KeyboardListener);
			base.DoInitialization();
		}
	}
}