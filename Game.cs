using Moggle.Screens;
using Moggle.Threading;
using System;

namespace Moggle
{
	/// <summary>
	/// Game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
		/// <summary>
		/// The screen threads manager for the game.
		/// </summary>
		public readonly ThreadManager Threads;
		/// <summary>
		/// Gets the current topmost screen.
		/// </summary>
		public IScreen CurrentScreen => Threads.ActiveThread.TopmostScreen;
		/// <summary>
		/// </summary>
		public Game()
		{
			Threads = new ThreadManager(this);
		}

		/// <summary>
		/// Invokes active thread's updater.
		/// </summary>
		protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.Update(gameTime);
			Threads.ActiveThread.UpdateRecursively(gameTime);
		}

		/// <summary>
		/// Invokes active thread's draw.
		/// </summary>
		protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.Draw(gameTime);
			Threads.ActiveThread.DrawRecursively();
		}

		/// <summary>
		/// Dispose the game.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			Threads.Dispose();
			base.Dispose(disposing);
		}

		/// <summary>
		/// Initialize the game.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			Initialized?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Occurs when the game has completed the initialization.
		/// </summary>
		public event EventHandler Initialized;
	}
}