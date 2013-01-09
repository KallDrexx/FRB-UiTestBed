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
using UiTestBed.Entities.XuiLikeDemo;
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

            if (LevelSelectMenuInstance.MenuExited)
            {
                LevelSelectMenuInstance.MenuExited = false;

                if (string.IsNullOrWhiteSpace(LevelSelectMenuInstance.LevelToLoad))
                {
                    MainMenuInstance.Activate();
                }
                else
                {
                    LoadingScreenInstance.LoadingText = "Loading Level: " + LevelSelectMenuInstance.LevelToLoad;
                    LoadingScreenInstance.Activate();
                }
            }

            else switch (MainMenuInstance.CurrentChosenMenuOptionState)
            {
                case MainMenu.ChosenMenuOption.Options:
                    MainMenuInstance.CurrentChosenMenuOptionState = MainMenu.ChosenMenuOption.None;
                    OptionsMenuInstance.Activate();
                    break;

                case MainMenu.ChosenMenuOption.LevelSelect:
                    MainMenuInstance.CurrentChosenMenuOptionState = MainMenu.ChosenMenuOption.None;
                    LevelSelectMenuInstance.Activate();
                    break;

                case MainMenu.ChosenMenuOption.Quit:
                    FlatRedBallServices.Game.Exit();
                    break;
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
