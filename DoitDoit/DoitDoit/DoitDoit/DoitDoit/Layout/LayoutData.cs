using Xamarin.Forms;

namespace Layout
{
	/// <summary>
	/// CODE FROM https://docs.microsoft.com/en-us/samples/xamarin/xamarin-forms-samples/userinterface-customlayout-wraplayout/
	/// 
	/// </summary>
	struct LayoutData
	{
		public int VisibleChildCount { get; private set; }

		public Size CellSize { get; private set; }

		public int Rows { get; private set; }

		public int Columns { get; private set; }

		public LayoutData(int visibleChildCount, Size cellSize, int rows, int columns)
		{
			VisibleChildCount = visibleChildCount;
			CellSize = cellSize;
			Rows = rows;
			Columns = columns;
		}
	}
}
