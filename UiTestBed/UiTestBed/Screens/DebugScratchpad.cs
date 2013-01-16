using FlatRedBall;
using FrbUi;
using FrbUi.Layouts;
#if FRB_XNA || SILVERLIGHT

#endif

namespace UiTestBed.Screens
{
	public partial class DebugScratchpad
	{

		void CustomInitialize()
		{
            var _grid = UiControlManager.Instance.CreateControl<GridLayout>();
            _grid.Spacing = 10;
            _grid.Margin = 40;
            _grid.BackgroundAnimationChains = GlobalContent.MenuBackground;
            _grid.CurrentAnimationChainName = "Idle";

		}

		void CustomActivity(bool firstTimeCalled)
		{


		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
