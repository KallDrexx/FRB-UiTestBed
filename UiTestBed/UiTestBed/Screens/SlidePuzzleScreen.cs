
using FrbUi;
using FrbUi.Controls;
using FrbUi.Layouts;
using FrbUi.Positioning;

namespace UiTestBed.Screens
{
	public partial class SlidePuzzleScreen
	{
	    private SimpleLayout _hudLayout;
	    private LayoutableText _moveCountText;
	    private int _lastMoveCountValue = -1;

		void CustomInitialize()
		{
		    _hudLayout = UiControlManager.Instance.CreateControl<SimpleLayout>();
            _hudLayout.FullScreen = true;

		    var scoreLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
		    scoreLayout.CurrentDirection = BoxLayout.Direction.Right;
		    scoreLayout.Spacing = 5;

		    var label = UiControlManager.Instance.CreateControl<LayoutableText>();
		    label.DisplayText = "Moves: ";
            scoreLayout.AddItem(label);

		    _moveCountText = UiControlManager.Instance.CreateControl<LayoutableText>();
		    _moveCountText.DisplayText = _lastMoveCountValue.ToString();
		    scoreLayout.AddItem(_moveCountText);

		    _hudLayout.AddItem(scoreLayout, HorizontalPosition.OffsetFromCenter(0), VerticalPosition.OffsetFromTop(-10));
		}

		void CustomActivity(bool firstTimeCalled)
		{
            if (_lastMoveCountValue != SlidePuzzleGridInstance.MoveCount)
            {
                _lastMoveCountValue = SlidePuzzleGridInstance.MoveCount;
                _moveCountText.DisplayText = SlidePuzzleGridInstance.MoveCount.ToString();
            }

            if (SlidePuzzleGridInstance.HasWon)
            {
                var wonText = UiControlManager.Instance.CreateControl<LayoutableText>();
                wonText.DisplayText = "You Won!";
                _hudLayout.AddItem(wonText, HorizontalPosition.OffsetFromCenter(0), VerticalPosition.OffsetFromTop(-30));
            }
		}

		void CustomDestroy()
		{
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {
        }

	}
}
