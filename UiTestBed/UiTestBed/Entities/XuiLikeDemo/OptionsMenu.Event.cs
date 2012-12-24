using System;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class OptionsMenu
	{	        
        void OnBeforeOverallAlphaSet (object sender, EventArgs e)
        {
            
        }

        void OnAfterOverallAlphaSet (object sender, EventArgs e)
        {
            if (_mainLayout != null)
                _mainLayout.Alpha = OverallAlpha;
        }

	}
}
