using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using FrbUi.Layouts;
using FlatRedBall.Instructions;
#endif

namespace UiTestBed.Screens
{
	public partial class MenuDemo
	{
		void CustomInitialize()
		{
		}

		void CustomActivity(bool firstTimeCalled)
		{
            if (firstTimeCalled)
                MainMenuInstance.Activate();

            if (OptionsMenuInstance.MenuExited)
            {
                OptionsMenuInstance.MenuExited = false;
                MainMenuInstance.Activate();
            }
            else if (MainMenuInstance.OptionsSelected)
            {
                MainMenuInstance.OptionsSelected = false;
                OptionsMenuInstance.Activate();
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
