using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using FrbUi;
using FrbUi.Controls;
using FrbUi.Layouts;
using FrbUi.Positioning;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

#endif

namespace UiTestBed.Entities.Tutorial
{
	public partial class BeefBallScoreHud
	{
        private LayoutableText _p1ScoreDisplay;
        private LayoutableText _p2ScoreDisplay;
	    private int _p1ScoreValue;
        private int _p2ScoreValue;

	    public int Player1Score
	    {
            get { return _p1ScoreValue; }
            set
            {
                _p1ScoreValue = value;
                _p1ScoreDisplay.DisplayText = value.ToString();
            }
	    }

	    public int Player2Score
	    {
            get { return _p2ScoreValue; }
            set
            {
                _p2ScoreValue = value;
                _p2ScoreDisplay.DisplayText = value.ToString();
            }
	    }

		private void CustomInitialize()
		{
            InitHud();
            _p1ScoreDisplay.DisplayText = "5";
            _p2ScoreDisplay.DisplayText = "23";
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void InitHud()
        {
            // Create the layout and make it take up the whole screen, and attach to the camera
            var  mainLayout = UiControlManager.Instance.CreateControl<SimpleLayout>();
            mainLayout.FullScreen = true;

            // Create the score value text controls
            _p1ScoreDisplay = UiControlManager.Instance.CreateControl<LayoutableText>();
            _p2ScoreDisplay = UiControlManager.Instance.CreateControl<LayoutableText>();

            // Create a layout that consists of the team label combined with the score value text
            var p1Layout = CreateScoreUi(_p1ScoreDisplay, 1);
            var p2Layout = CreateScoreUi(_p2ScoreDisplay, 2);

            // Add the items to the main layout, putting team 1 score 25% from the left border and 10 units below the top
            //    and place team 2's score 25% from the right and 10 units below the top border
            mainLayout.AddItem(p1Layout, HorizontalPosition.PercentFromLeft(25), VerticalPosition.OffsetFromTop(-10), LayoutOrigin.TopLeft);
            mainLayout.AddItem(p2Layout, HorizontalPosition.PercentFromRight(-25), VerticalPosition.OffsetFromTop(-10), LayoutOrigin.TopRight);
        }

        private BoxLayout CreateScoreUi(LayoutableText scoreValueText, int teamNum)
        {
            // Create the layout that holds the score label and value
            var scoreLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            scoreLayout.CurrentDirection = BoxLayout.Direction.Right;
            scoreLayout.Spacing = 5;

            // Setup the label's text control
            var label = UiControlManager.Instance.CreateControl<LayoutableText>();
            label.DisplayText = "Team " + teamNum + ":";
            scoreLayout.AddItem(label);

            // Set the value to default to zero and add it to the layout
            scoreValueText.DisplayText = "0";
            scoreLayout.AddItem(scoreValueText);

            return scoreLayout;
        }
	}
}
