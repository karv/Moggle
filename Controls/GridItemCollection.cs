using System;
using System.Collections.ObjectModel;

namespace Moggle.Controls
{
	/// <summary>
	/// A collection whose items can be accessed via 2-dim indexers.
	/// </summary>
	public class GridItemCollection<T> : ObservableCollection<T>
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