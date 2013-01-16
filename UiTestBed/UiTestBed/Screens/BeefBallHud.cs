using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
using FrbUi;
using FrbUi.Layouts;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;
using FrbUi.Controls;
using FrbUi.Positioning;

namespace UiTestBed.Screens
{
	public partial class BeefBallHud
	{
		void CustomInitialize()
		{
		    BeefBallScoreHudInstance.Player1Score = 2;
		    BeefBallScoreHudInstance.Player2Score = 15;
		    BeefBallScoreHudInstance.Player2Score = (BeefBallScoreHudInstance.Player2Score + 2);
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
