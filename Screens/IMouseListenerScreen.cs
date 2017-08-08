using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Screens
{
	public interface IMouseListenerScreen : IScreen
	{
		MouseListener Mouse { get; }
	}
}