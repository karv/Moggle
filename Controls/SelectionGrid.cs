using System;
using System.Collections.Generic;
using CE.Collections.Selector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Controls;
using Moggle.Screens;
using MonoGame.Extended.Input.InputListeners;

namespace Civo.Systems.Controls.General
{
	/// <summary>
	/// A generic display of items in a grid.
	/// </summary>
	public class SelectionGrid<T> : ClickableControl, Moggle.IDrawable
	{
		readonly List<Texture2D> _textures = new List<Texture2D>();
		Func<T, Texture2D> _textureSelector;
		/// <summary>
		/// The selection manager.
		/// </summary>
		public readonly SelectionManager<T> Selection;
		/// <summary>
		/// The items.
		/// </summary>
		public readonly GridItemCollection<T> Items = new GridItemCollection<T>();
		/// <summary>
		/// The topleft location.
		/// </summary>
		public Point Location;
		/// <summary>
		/// The size if each tile.
		/// </summary>
		public CE.Size TileSize;
		/// <summary>
		/// The size, in tiles, of the grid.
		/// </summary>
		/// <value>The size of the grid.</value>
		public CE.Size GridSize { get => Items.GridSize; set => Items.GridSize = value; }
		bool _allowSelection = true;

		/// <param name="mouse">Mouse.</param>
		public SelectionGrid(MouseListener mouse) : base(mouse)
		{
			Selection = new SelectionManager<T>(Items, new TrivialSelectionRestrain<T>());
		}

		/// <param name="mouse">Mouse.</param>
		/// <param name="selRestrain">Selection restrain.</param>
		public SelectionGrid(MouseListener mouse, ISelectionRestrain<T> selRestrain) : base(mouse)
		{
			Selection = new SelectionManager<T>(Items, selRestrain);
		}

		/// <param name="screen">Screen.</param>
		public SelectionGrid(ListenerScreen screen) : base(screen.MouseListener)
		{
			Selection = new SelectionManager<T>(Items, new TrivialSelectionRestrain<T>());
		}

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
		/// Gets or sets a value indicating whether this <see cref="T:Civo.Systems.Controls.General.SelectionGrid`1"/> 
		/// allows selection.
		/// </summary>
		public bool AllowSelection
		{
			get => _allowSelection;
			set
			{
				if (value == _allowSelection) return;

				_allowSelection = value;
				if (value == false)
					Selection.ClearSelection();
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
			_textures.Clear();
			for (int i = 0; i < Items.Count; i++)
				_textures.Add(TextureSelector(Items[i]));
		}


		/// <summary>
		/// Suscribes to <see cref="Items"/> events and build textures.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			Items.CollectionChanged += (sender, e) => RebuildTextures();
			RebuildTextures();
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
		/// Converts a relative position to a tile postion.
		/// </summary>
		public CE.Point PointToTile(Point p) => new CE.Point(p.X / TileSize.Width, p.Y / TileSize.Height);

		/// <summary>
		/// Gets the drawing rectangle that correcponds a specified tile.
		/// </summary>
		protected Rectangle TileToRectangle(CE.Point p) => new Rectangle(Location.X + p.X * TileSize.Width,
																																		 Location.Y + p.Y * TileSize.Height,
																																		 TileSize.Width,
																																		 TileSize.Height);
		/// <summary>
		/// Invokes the <see cref="ItemClicked"/> event.
		/// </summary>
		/// <param name="e">Mouse state event args</param>
		/// <param name="itemIndex">index of the clicked item.</param>
		protected virtual void OnItemClicked(ControlMouseEventArgs e, int itemIndex)
		{
			var item = Items[itemIndex];
			if (AllowSelection)
				Selection.ToggleSelection(item);
			ItemClicked?.Invoke(this, item);
		}

		void Moggle.IDrawable.Draw(SpriteBatch batch)
		{
			// TODO: store the textures in a hidden field.
			for (int i = 0; i < Items.Count; i++)
			{
				var outputRect = TileToRectangle(Items.IndexToGrid(i));
				batch.Draw(_textures[i], outputRect, Color.White);
			}
		}


		/// <summary>
		/// Occurs when any item is clicked.
		/// </summary>
		public event EventHandler<T> ItemClicked;
	}
}