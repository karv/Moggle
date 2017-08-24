using CE.Collections.Selector;
using Moggle.Screens;
using MonoGame.Extended.Input.InputListeners;

namespace Moggle.Controls
{
	/// <summary>
	/// A generic display of items in a grid.
	/// </summary>
	public class SelectionGrid<T> : Grid<T>, IDrawable
	{
		/// <summary>
		/// The selection manager.
		/// </summary>
		public readonly SelectionManager<T> Selection;
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
		public SelectionGrid(ListenerScreen screen) : base(screen)
		{
			Selection = new SelectionManager<T>(Items, new TrivialSelectionRestrain<T>());
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
		/// Invokes the <see cref="E:Moggle.Controls.Grid`1.ItemClicked" /> event
		/// and updates selection
		/// </summary>
		/// <param name="e">Mouse state event args</param>
		/// <param name="itemIndex">index of the clicked item.</param>
		protected override void OnItemClicked(ControlMouseEventArgs e, int itemIndex)
		{
			if (AllowSelection)
				Selection.ToggleSelection(Items[itemIndex]);
			base.OnItemClicked(e, itemIndex);
		}
	}
}