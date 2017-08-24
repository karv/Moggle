using System;
using Microsoft.Xna.Framework;
using Moggle.Screens;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	public class PickingGrid<T> : Grid<T>
	{
		public int _selectedIndex;
		public Color SelectedBorderColor = Color.Yellow;
		public int SelectedBorderThick = 3;
		public PickingGrid(MouseListener mouse) : base(mouse) { }
		public PickingGrid(ListenerScreen screen) : base(screen.MouseListener) { }

		public int SelectedIndex
		{
			get => _selectedIndex;
			set
			{
				_selectedIndex = value;
				SelectionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		protected override void DrawItem(Microsoft.Xna.Framework.Graphics.SpriteBatch batch,
																		 int index,
																		 Rectangle output)
		{
			base.DrawItem(batch, index, output);
			if (index == SelectedIndex)
				batch.DrawRectangle(output, SelectedBorderColor, SelectedBorderThick);
		}

		protected override void OnItemClicked(ControlMouseEventArgs e, int itemIndex)
		{
			SelectedIndex = itemIndex;
			base.OnItemClicked(e, itemIndex);
		}

		public event EventHandler SelectionChanged;
	}
}