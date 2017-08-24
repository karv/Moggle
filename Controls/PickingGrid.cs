using System;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	/// <summary>
	/// A grid that will let the user select an item from a grid.
	/// </summary>
	public class PickingGrid<T> : Grid<T>
	{
		int _selectedIndex;
		/// <summary>
		/// Color of the border, if selected.
		/// </summary>
		public Color SelectedBorderColor = Color.Yellow;
		/// <summary>
		/// Tickness of the border, if selected.
		/// </summary>
		public int SelectedBorderThick = 3;

		/// <param name="mouse">Mouse Listener</param>
		public PickingGrid(MouseListener mouse) : base(mouse) { }
		/// <param name="screen">Screen</param>
		public PickingGrid(ListenerScreen screen) : base(screen.MouseListener) { }

		/// <summary>
		/// Gets or sets the index of the selected item.
		/// </summary>
		/// <value>The index of the selected.</value>
		public int SelectedIndex
		{
			get => _selectedIndex;
			set
			{
				_selectedIndex = value;
				SelectionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Draws the item.
		/// </summary>
		/// <param name="batch">Drawing batch.</param>
		/// <param name="index">Index of the currently drawing item.</param>
		/// <param name="output">The location of the rendering.</param>
		protected override void DrawItem(Microsoft.Xna.Framework.Graphics.SpriteBatch batch,
																		 int index,
																		 Rectangle output)
		{
			base.DrawItem(batch, index, output);
			if (index == SelectedIndex)
				batch.DrawRectangle(output, SelectedBorderColor, SelectedBorderThick);
		}


		/// <summary>
		/// Invokes the <see cref="E:Moggle.Controls.Grid`1.ItemClicked" /> event, and updates the selected item.
		/// </summary>
		/// <param name="e">Mouse state event args</param>
		/// <param name="itemIndex">index of the clicked item.</param>
		protected override void OnItemClicked(ControlMouseEventArgs e, int itemIndex)
		{
			SelectedIndex = itemIndex;
			base.OnItemClicked(e, itemIndex);
		}

		/// <summary>
		/// Occurs when the selection changes.
		/// </summary>
		public event EventHandler SelectionChanged;
	}
}