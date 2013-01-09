using System;
using FrbUi.Layouts;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class LevelSelectMenu
	{
		void OnAfterSelectedGridSpacingSet (object sender, EventArgs e)
		{
            if (_mainGroup == null)
                return;

		    var currentGrid = _mainGroup.CurrentlyFocusedItem as GridLayout;
            if (currentGrid != null)
            {
                currentGrid.Spacing = SelectedGridSpacing;
            }
		}        
        
        void OnAfterOverallAlphaSet (object sender, EventArgs e)
        {
            if (_gridSelectableGroups == null)
                return;

            foreach (GridLayout grid in _gridSelectableGroups.Keys)
                grid.Alpha = OverallAlpha;
        }

	}
}
