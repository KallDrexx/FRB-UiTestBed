using System;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class LoadingScreen
	{
		        
        void OnAfterOverallAlphaSet (object sender, EventArgs e)
        {
            if (_layout == null)
                return;

            _layout.Alpha = OverallAlpha;
        }

        void OnAfterLoadingTextSet (object sender, EventArgs e)
        {
            if (_text == null)
                return;

            _text.DisplayText = string.IsNullOrWhiteSpace(LoadingText) 
                ? "Loading, Please Wait..." 
                : LoadingText;
        }
	}
}
