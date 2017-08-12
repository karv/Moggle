using System.Diagnostics;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using OpenTK.Input;
using Moggle.Controls;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework.Graphics;

namespace Civo
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game : Moggle.Game
	{
		ListenerScreen _scr1;
		GraphicsDeviceManager _graphics;
		Texture2D _solidTexture;

		/// <summary>
		/// </summary>
		public Game()
		{
			_graphics = new GraphicsDeviceManager(this);
			IsMouseVisible = true;
		}
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		protected override void Initialize()
		{
			_solidTexture = new Texture2D(GraphicsDevice, 1, 1);
			_solidTexture.SetData(new[] { Color.White });

			_scr1 = new ListenerScreen(this);
			var ctrl = new Button(_scr1.MouseListener, _solidTexture) { Bounds = new Rectangle(0, 0, 100, 100) };
			ctrl.Clicked += delegate
			{
				Debug.Write("Click");
			};
			_scr1.Components.Add(ctrl);

			_graphics.IsFullScreen = false;
			Content.RootDirectory = "Content";

			var thr = Threads.CreateAndSwitch();
			thr.BackgroundColor = Color.Red;

			thr.Stack(_scr1);
			base.Initialize();
		}
	}
}