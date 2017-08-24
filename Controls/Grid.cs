using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Screens;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	public abstract class Grid<T> : ClickableControl, IDrawable
	{
		protected readonly List<Texture2D> Textures = new List<Texture2D>();
		Func<T, Texture2D> _textureSelector;
		/// <summary>
		/// The size if each tile.
		/// </summary>
		public CE.Size TileSize;
		/// <summary>
		/// The size, in tiles, of the grid.
		/// </summary>
		/// <value>The size of the grid.</value>
		public CE.Size GridSize { get => Items.GridSize; set => Items.GridSize = value; }
		/// <summary>
		/// The items.
		/// </summary>
		public readonly GridItemCollection<T> Items = new GridItemCollection<T>();
		/// <summary>
		/// The topleft location.
		/// </summary>
		public Point Location;

		protected Grid(MouseListener mouse) : base(mouse) { }
		protected Grid(ListenerScreen screen) : base(screen.MouseListener) { }

		/// <summary>
		/// The bounds of the listening area.
		/// </summary>
		protected override Rectangle Bounds
		{
			get
			{
				var size = new Point(TileSize.Width * Items.GridSize.Width, TileSize.Height * Items.GridSize.Height);
				return new Rectangle(Location, size);
			}
		}

		/// <summary>
		/// Gets or sets the texture selector.
		/// </summary>
		/// <value>The texture selector.</value>
		public Func<T, Texture2D> TextureSelector
		{
			get => _textureSelector;
			set
			{
				_textureSelector = value ?? throw new ArgumentNullException();
				RebuildTextures();
			}
		}

		/// <summary>
		/// Rebuilds the textures of the objects
		/// </summary>
		protected void RebuildTextures()
		{
			Textures.Clear();
			for (int i = 0; i < Items.Count; i++)
				Textures.Add(TextureSelector(Items[i]));
		}

		/// <summary>
		/// Suscribes to mouse events and load textures.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			RebuildTextures();
			Items.CollectionChanged += LoadTextureEvent;
		}

		protected override void Dispose()
		{
			Items.CollectionChanged -= LoadTextureEvent;
			base.Dispose();
		}

		/// <summary>
		/// Invoked when the object is clicked.
		/// Determines which tile has been clicked.
		/// </summary>
		protected override void OnClick(ControlMouseEventArgs e)
		{
			var TilePos = PointToTile(e.RelativePosition);
			var clickIndex = Items.GridToIndex(TilePos);

			if (clickIndex < Items.Count)
				OnItemClicked(e, clickIndex);
		}

		/// <summary>
		/// Invokes the <see cref="ItemClicked"/> event.
		/// </summary>
		/// <param name="e">Mouse state event args</param>
		/// <param name="itemIndex">index of the clicked item.</param>
		protected virtual void OnItemClicked(ControlMouseEventArgs e, int itemIndex)
		{
			ItemClicked?.Invoke(this, Items[itemIndex]);
		}


		/// <summary>
		/// Converts a relative position to a tile postion.
		/// </summary>
		public CE.Point PointToTile(Point p) => new CE.Point(p.X / TileSize.Width, p.Y / TileSize.Height);

		void IDrawable.Draw(SpriteBatch batch)
		{
			for (int i = 0; i < Items.Count; i++)
			{
				var outputRect = TileToRectangle(Items.IndexToGrid(i));
				DrawItem(batch, i, outputRect);
			}
		}

		protected virtual void DrawItem(SpriteBatch batch, int index, Rectangle output)
		{
			batch.Draw(Textures[index], output, Color.White);
		}

		/// <summary>
		/// Gets the drawing rectangle that correcponds a specified tile.
		/// </summary>
		protected Rectangle TileToRectangle(CE.Point p) => new Rectangle(Location.X + p.X * TileSize.Width,
																																		 Location.Y + p.Y * TileSize.Height,
																																		 TileSize.Width,
																																		 TileSize.Height);

		void LoadTextureEvent(object sender, EventArgs e) => RebuildTextures();

		/// <summary>
		/// Occurs when any item is clicked.
		/// </summary>
		public event EventHandler<T> ItemClicked;
	}
}
