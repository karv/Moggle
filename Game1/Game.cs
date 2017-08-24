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

		Texture2D SingleColorTexture(Color color)
		{
			var ret = new Texture2D(GraphicsDevice, 1, 1);
			ret.SetData(new[] { color });
			return ret;
		}
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			_solidTexture = SingleColorTexture(Color.White);
			_scr1 = new ListenerScreen(this)
			{ ScreenViewport = new ScalingViewportAdapter(GraphicsDevice, 100, 100) };
			_scr1.Initialize();
			var ctrl = new Button(_scr1.MouseListener, _solidTexture) { Location = new Rectangle(0, 0, 10, 10) };
			var grid = new PickingGrid<Color>(_scr1.MouseListener)
			{
				TextureSelector = SingleColorTexture,
				Location = new Point(10, 0),
				GridSize = new CE.Size(2, 2),
				TileSize = new CE.Size(5, 5),
				SelectedBorderThick = 1
			};
			grid.Items.Add(Color.Red);
			grid.Items.Add(Color.Green);
			grid.Items.Add(Color.Blue);
			ctrl.Clicked += delegate
			{
				Debug.Write("Click");
			};
			grid.SelectionChanged += delegate
			{
				Debug.WriteLine(grid.Items[grid.SelectedIndex]);
			};
			_scr1.AddComponent(grid);
			_scr1.AddComponent(ctrl);

			_graphics.IsFullScreen = false;
			Content.RootDirectory = "Content";

			var thr = Threads.CreateAndSwitch();
			thr.BackgroundColor = Color.DarkGray;

			thr.Stack(_scr1);
		}
	}
}