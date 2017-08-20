using System;
using System.Collections.ObjectModel;
using Moggle.Controls;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Moggle.Screens;

namespace Civo.Systems.Controls.General
{
	/// <summary>
	/// A generic display of items in a grid.
	/// </summary>
	public class SelectionGrid<T> : ClickableControl
	{
		/// <summary>
		/// The items.
		/// </summary>
		public readonly ItemCollection<T> Items;
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

		/// <param name="mouse">Mouse.</param>
		public SelectionGrid(MouseListener mouse) : base(mouse)
		{
			Items = new ItemCollection();
		}

		/// <param name="screen">Screen.</param>
		public SelectionGrid(ListenerScreen screen) : base(screen.MouseListener)
		{
			Items = new ItemCollection<T>();
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
		/// Invoked when the object is clicked.
		/// Determines which tile has been clicked.
		/// </summary>
		protected override void OnClick(MouseEventArgs e) { }
	}
	public class ItemCollection<T> : Collection<T>
	{
		/// <summary>
		/// The size of the grid.
		/// </summary>
		public CE.Size GridSize;
		LexicographicalOrder _order;

		/// <summary>
		/// Gets or sets the order.
		/// </summary>
		public LexicographicalOrder Order
		{
			get => _order;
			set => _order = value;
		}

		/// <summary>
		/// Gets the value at a specified point.
		/// </summary>
		/// <param name="x">The x index.</param>
		/// <param name="y">The y index.</param>
		public T this[int x, int y] => this[new CE.Point(x, y)];
		/// <summary>
		/// Gets the value at a specified point.
		/// </summary>
		/// <param name="p">The 2D index.</param>
		public T this[CE.Point p] => this[GridToIndex(p)];
		/// <summary>
		/// Converts 2D index to linear index.
		/// </summary>
		public int GridToIndex(CE.Point p)
		{
			switch (Order)
			{
				case LexicographicalOrder.Natural:
					return p.X + p.Y * GridSize.Width;
				case LexicographicalOrder.Inverted:
					return p.Y + p.X * GridSize.Height;
			}
			throw new Exception();
		}

		/// <summary>
		/// Converts linear index to 2D index.
		/// </summary>
		public CE.Point IndexToGrid(int i)
		{
			if (i < 0) throw new IndexOutOfRangeException();
			switch (Order)
			{
				case LexicographicalOrder.Natural:
					return new CE.Point(i % GridSize.Width, i / GridSize.Width);
				case LexicographicalOrder.Inverted:
					return new CE.Point(i / GridSize.Height, i % GridSize.Height);
			}
			throw new Exception();
		}
	}

}