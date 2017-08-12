using System.Diagnostics;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using OpenTK.Input;

namespace Civo
{
	class DebugScreen : Screen
	{
		public readonly string Name;
		public DebugScreen(Game game, string name) : base(game)
		{
			Name = name;
		}

		protected override void DoInitialization()
		{
			base.DoInitialization();
			Debug.WriteLine("Initialized " + Name);
		}

		public override void Update(GameTime gameTime)
		{
			Debug.WriteLine("Update " + Name);
			base.Update(gameTime);
			if (Keyboard.GetState().IsAnyKeyDown)
				Game.Threads.ActiveThread.TerminateLast();
		}
	}

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game : Moggle.Game
	{
		Screen _scr1;
		Screen _scr2;
		GraphicsDeviceManager _graphics;

		/// <summary>
		/// </summary>
		public Game()
		{
			_graphics = new GraphicsDeviceManager(this);
		}
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		protected override void Initialize()
		{
			_scr1 = new DebugScreen(this, "1");
			_scr2 = new DebugScreen(this, "2");
			_graphics.IsFullScreen = false;
			Content.RootDirectory = "Content";

			var thr = Threads.CreateAndSwitch();
			thr.BackgroundColor = Color.AliceBlue;

			thr.Stack(_scr1);
			thr.Stack(_scr2);
			base.Initialize();
		}
	}
}