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
            // Create a 5 by 5 grid of buttons
            _buttons = new UiButton[WIDTH, HEIGHT];
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    var newBtn = CreateButton();
                    _buttons[x, y] = newBtn;
                    newBtn.X = (x * 5);
                    newBtn.Y = (y * 5);
                    newBtn.Text = string.Concat("(", x, ", ", y, ")");
                    newBtn.Alpha = 0;

                    // Add adjacent buttons
                    if (x > 0)
                        newBtn.SetAdjacentButton(_buttons[x - 1, y], ButtonDirection.Left);

                    if (y > 0)
                        newBtn.SetAdjacentButton(_buttons[x, y - 1], ButtonDirection.Down);
                }
            }

            _currentButton = UiButtonList[0];
            _currentButton.Select();
            _currentButton.Alpha = 1;
		}

        void CustomActivity(bool firstTimeCalled)
		{
            if (InputManager.Keyboard.KeyPushed(Keys.Right))
                _currentButton.SelectAdjacentButton(ButtonDirection.Right);
            else if (InputManager.Keyboard.KeyPushed(Keys.Left))
                _currentButton.SelectAdjacentButton(ButtonDirection.Left);
            else if (InputManager.Keyboard.KeyPushed(Keys.Up))
                _currentButton.SelectAdjacentButton(ButtonDirection.Up);
            else if (InputManager.Keyboard.KeyPushed(Keys.Down))
                _currentButton.SelectAdjacentButton(ButtonDirection.Down);
		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private UiButton CreateButton()
        {
            var onSelected = new ButtonEventHandler(delegate(object sender)
            {
                var btn = sender as UiButton;
                if (btn != null)
                {
                    _currentButton = btn;
                    btn.Text = "Selected";
                    InstructionManager.MoveToAccurate(SpriteManager.Camera, btn.X, btn.Y, SpriteManager.Camera.Z, 0.25);
                    FadeButtons(btn);
                }
            });

            var onUnSelected = new ButtonEventHandler(delegate(object sender)
            {
                var btn = sender as UiButton;
                if (btn != null)
                    btn.Text = "Not Selected";
            });

            var onPressed = new ButtonEventHandler(delegate(object sender)
            {
                var btn = sender as UiButton;
                if (btn != null)
                    btn.Text = "Pressed";
            });

            var onRelease = new ButtonEventHandler(delegate(object sender)
            {
                var btn = sender as UiButton;
                if (btn != null)
                    btn.Text = "Released";
            });

            var newBtn = new UiButton(ContentManagerName);
            newBtn.Text = "Test 1234";

            newBtn.OnSelectedHandler += onSelected;
            newBtn.OnUnSelectedHandler += onUnSelected;
            newBtn.OnPressedHandler += onPressed;
            newBtn.OnReleasedHandler += onRelease;

            UiButtonList.Add(newBtn);
            return newBtn;
        }

        private void FadeButtons(UiButton selectedButton)
        {
            if (selectedButton == null)
                return;

            // Get directional buttons
            var up = selectedButton.GetAdjacentButton(ButtonDirection.Up);
            var down = selectedButton.GetAdjacentButton(ButtonDirection.Down);
            var left = selectedButton.GetAdjacentButton(ButtonDirection.Left);
            var right = selectedButton.GetAdjacentButton(ButtonDirection.Right);

            // Set all buttons adjacent to the selected button to fade in
            FadeButtonIn(up);
            FadeButtonIn(down);
            FadeButtonIn(left);
            FadeButtonIn(right);

            // Make sure all other buttons are faded out
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    var curBtn = _buttons[x, y];
                    if (curBtn != up && curBtn != down && curBtn != left && curBtn != right && curBtn != selectedButton)
                        FadeButtonOut(curBtn);
                }
            }
        }

        private void FadeButtonIn(UiButton btn)
        {
            if (btn == null)
                return;

            // Check if this button is already visible
            if (btn.Alpha == 1)
                return;

            btn.Alpha = 0;
            new Instruction<UiButton, float>(btn, "Alpha", 1, TimeManager.CurrentTime + 1).Execute();
        }

        private void FadeButtonOut(UiButton btn)
        {
            if (btn == null)
                return;

            // Check if this button is already invisible
            if (btn.Alpha == 0)
                return;

            btn.Alpha = 1;
            new Instruction<UiButton, float>(btn, "Alpha", 0, TimeManager.CurrentTime + 1).Execute();
        }
	}
}
