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
#endif

using UiTestBed.Entities;
using FlatRedBall.Instructions;

namespace UiTestBed.Screens
{
	public partial class ButtonTestScreen1
	{
        private const int WIDTH = 5;
        private const int HEIGHT = 5;

        private UiButton _currentButton;
        private UiButton[,] _buttons;

		void CustomInitialize()
		{
            var rand = new Random();

            // Create a 5 by 5 grid of buttons
            _buttons = new UiButton[WIDTH, HEIGHT];
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    var newBtn = CreateButton();
                    _buttons[x, y] = newBtn;
                    newBtn.X = (x * 100);
                    newBtn.Y = (y * 100);
                    newBtn.Text = string.Concat("(", x, ", ", y, ")");
                    newBtn.ResizeAroundText(5, 5);
                }
            }

            _currentButton = UiButtonList[0];
            _currentButton.Select();
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

        private UiButton CreateButton()
        {
            var onSelected = new ButtonEventHandler(delegate(UiButton sender)
            {
                if (sender != null)
                {
                    _currentButton = sender;
                    sender.Text = "Selected";
                    //InstructionManager.MoveToAccurate(SpriteManager.Camera, sender.X, sender.Y, SpriteManager.Camera.Z, 0.25);
                }
            });

            var onUnSelected = new ButtonEventHandler(delegate(UiButton sender)
            {
                if (sender != null)
                {
                    sender.Text = "Not Selected";
                    sender.ResizeAroundText(5, 5);
                }
            });

            var onPressed = new ButtonEventHandler(delegate(UiButton sender)
            {
                if (sender != null)
                    sender.Text = "Pressed";
            });

            var onRelease = new ButtonEventHandler(delegate(UiButton sender)
            {
                if (sender != null)
                    sender.Text = "Released";
            });

            var newBtn = new UiButton(ContentManagerName, false);
            newBtn.AddToManagers(UiLayer);
            newBtn.Text = "Test 1234";
            newBtn.SpriteScaleX = newBtn.SpriteTexture.Width * newBtn.SpritePixelSize;
            newBtn.SpriteScaleY = newBtn.SpriteTexture.Height * newBtn.SpritePixelSize;

            newBtn.OnSelectedHandler += onSelected;
            newBtn.OnUnSelectedHandler += onUnSelected;
            newBtn.OnPressedHandler += onPressed;
            newBtn.OnReleasedHandler += onRelease;

            UiButtonList.Add(newBtn);
            return newBtn;
        }
	}
}
