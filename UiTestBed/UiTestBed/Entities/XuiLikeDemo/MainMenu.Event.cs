using System;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class MainMenu
	{
	    private bool _previousActiveState;

		void OnAfterIsActiveSet (object sender, EventArgs e)
        {
            // Only do something if the previous active status actually changed
            if (_previousActiveState != IsActive)
            {
                if (IsActive)
                    _mainGroup.FocusNextControl();
                else
                    _mainGroup.UnfocusCurrentControl();
            }
        }        
        
        void OnBeforeIsActiveSet (object sender, EventArgs e)
        {
            _previousActiveState = IsActive;
        }

        void OnAfterOverallAlphaSet (object sender, EventArgs e)
        {
            if (_layout != null)
                _layout.Alpha = OverallAlpha;
            
        }
	}
}
