namespace Utilities
{
	using System.Collections.Generic;
	using System.Windows.Forms;

	public sealed class ExtendedPropertyGrid : PropertyGrid
	{
		private static void AddItem(GridItem item, ICollection<GridItem> gridItems)
		{
			switch(item.GridItemType)
			{
				case GridItemType.Category:
				case GridItemType.Root:
					foreach(GridItem i in item.GridItems)
						AddItem(i, gridItems);
					break;
				case GridItemType.Property:
					gridItems.Add(item);
					if(item.Expanded)
					{
						foreach(GridItem i in item.GridItems)
							AddItem(i, gridItems);
					}
					break;
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if((keyData & Keys.Tab) == Keys.Tab)
			{
				var gridItems = new List<GridItem>();
				var root = SelectedGridItem;
				if(root != null)
				{
					while(root.GridItemType != GridItemType.Root) root = root.Parent;
					AddItem(root, gridItems);
				}

				var index = gridItems.IndexOf(SelectedGridItem);

				if((keyData & Keys.Shift) == Keys.Shift)
				{
					if(index != 0)
						SelectedGridItem = gridItems[index - 1];
					else
						return base.ProcessCmdKey(ref msg, keyData);
				}
				else
				{
					if(index != gridItems.Count - 1)
						SelectedGridItem = gridItems[index + 1];
					else
						return base.ProcessCmdKey(ref msg, keyData);
				}

				if(SelectedGridItem.Expandable && !SelectedGridItem.Expanded)
					SelectedGridItem.Expanded = true;

				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
