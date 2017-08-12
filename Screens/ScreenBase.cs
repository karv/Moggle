using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Moggle.Screens
{
	/// <summary>
	/// A common implementation for a <see cref="IScreen"/>.
	/// </summary>
	public abstract class ScreenBase : IScreen
	{
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Moggle.Screens.ScreenBase"/> is initialized.
		/// </summary>
		/// <value><c>true</c> if is initialized; otherwise, <c>false</c>.</value>
		protected bool IsInitialized { get; private set; }
		/// <summary>
		/// The components
		/// </summary>
		protected readonly GameComponentCollection Components;
		/// <summary>
		/// The screen viewport. If set to null, no viewport will be used.
		/// </summary>
		public ViewportAdapter ScreenViewport;
		/// <summary>
		/// The game.
		/// </summary>
		public readonly Game Game;
		/// <summary>
		/// Gets the drawing batch.
		/// </summary>
		/// <value>The batch.</value>
		protected SpriteBatch Batch { get; private set; }

		/// <summary>
		/// Gets the component count.
		/// </summary>
		public int ComponentCount => Components.Count;

		/// <param name="game">Game.</param>
		protected ScreenBase(Game game)
		{
			Game = game ?? throw new System.ArgumentNullException(nameof(game));
			Components = new GameComponentCollection();
		}

		/// <summary>
		/// Initialize this screen
		/// </summary>
		public void Initialize()
		{
			if (IsInitialized) return;
			IsInitialized = true;
			DoInitialization();
		}

		/// <summary>
		/// Add a component
		/// </summary>
		public void AddComponent(IGameComponent component)
		{
			Components.Add(component);
			if (IsInitialized) component.Initialize();
		}

		/// <summary>
		/// Remove and dispose a component
		/// </summary>
		public void DestroyComponents(IGameComponent component)
		{
			if (Components.Remove(component))
				(component as IDisposable)?.Dispose();
		}

		/// <summary>
		/// Executes the initialization.
		/// This method is controlled by the <see cref="IsInitialized"/> flag, so it will only be called once.
		/// </summary>
		protected virtual void DoInitialization()
		{
			Batch = new SpriteBatch(Game.GraphicsDevice);
			foreach (var z in Components)
				z.Initialize();
		}

		/// <summary>
		/// Draws this screen and its components.
		/// </summary>
		public virtual void Draw()
		{
			if (Game.IsActive)
			{
				Batch.Begin(transformMatrix: ScreenViewport?.GetScaleMatrix() ?? null);
				foreach (var z in Components.OfType<IDrawable>())
					z.Draw(Batch);
				Batch.End();
			}
		}

		/// <summary>
		/// Updates this screen and its components.
		/// </summary>
		public virtual void Update(GameTime gameTime)
		{
			if (Game.IsActive)
				foreach (var z in Components.OfType<IUpdate>())
					z.Update(gameTime);
		}

	}
}