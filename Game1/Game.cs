using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Controls;
using Moggle.Screens;
using MonoGame.Extended.ViewportAdapters;

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
			base.Initialize();
			_solidTexture = new Texture2D(GraphicsDevice, 1, 1);
			_solidTexture.SetData(new[] { Color.White });

			_scr1 = new ListenerScreen(this)
			{ ScreenViewport = new ScalingViewportAdapter(GraphicsDevice, 100, 100) };
			_scr1.Initialize();
			var ctrl = new Button(_scr1.MouseListener, _solidTexture) { Bounds = new Rectangle(0, 0, 50, 50) };
			ctrl.Clicked += delegate
			{
				Debug.Write("Click");
			};
			_scr1.AddComponent(ctrl);

			_graphics.IsFullScreen = false;
			Content.RootDirectory = "Content";

			var thr = Threads.CreateAndSwitch();
			thr.BackgroundColor = Color.Red;

			thr.Stack(_scr1);
		}
	}
}