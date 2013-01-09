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

using FlatRedBall.Instructions;
using FlatRedBall.Graphics;
using FrbUi.Controls;
using FrbUi;
using FrbUi.Positioning;
using FrbUi.Layouts;

namespace UiTestBed.Screens
{
	public partial class ButtonTestScreen1
	{
        private SimpleLayout _mainLayout;

		void CustomInitialize()
		{
            _mainLayout = UiControlManager.Instance.CreateControl<SimpleLayout>();

            var grid = UiControlManager.Instance.CreateControl<GridLayout>();
            grid.Margin = 20;
            grid.Spacing = 30;

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (col == 2 && row == 2)
                    {
                        var circle = UiControlManager.Instance.CreateControl<CircularLayout>();
                        circle.StartingDegrees = 90;
                        circle.Radius = 80;
                        circle.CurrentArrangementMode = CircularLayout.ArrangementMode.EvenlySpaced;
                        circle.ShowBorder = false;

                        for (int x = 0; x < 5; x++)
                        {
                            var btn = CreateButton();
                            btn.Text = "#" + x;
                            btn.ResizeAroundText(5, 5);
                            circle.AddItem(btn);
                        }

                        grid.AddItem(circle, row, col, horizontalAlignment: GridLayout.HorizontalAlignment.Center, verticalAlignment: GridLayout.VerticalAlignment.Center);
                    }
                    else
                    {
                        var btn = CreateButton();
                        btn.Text = string.Format("Button {0} - {1}", row, col);
                        btn.ResizeAroundText(10, 10);
                        grid.AddItem(btn, row, col, horizontalAlignment: GridLayout.HorizontalAlignment.Center, verticalAlignment: GridLayout.VerticalAlignment.Center);
                    }
                }
            }

            _mainLayout.FullScreen = true;
            _mainLayout.AddItem(grid, HorizontalPosition.PercentFromLeft(5), VerticalPosition.PercentFromTop(-5), LayoutOrigin.TopLeft);
		}

        private void CreateButtonsForLayout(BoxLayout layout)
        {
            for (int x = 0; x < 5; x++)
            {
                var btn = CreateButton();
                btn.Text = "Button # " + x;
                btn.ResizeAroundText(5, 5);
                layout.AddItem(btn);
            }
        }

        void CustomActivity(bool firstTimeCalled)
		{
            if (InputManager.Keyboard.KeyPushed(Keys.F1))
                MoveToScreen(typeof(Screen2));
		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private static Button CreateButton()
        {
            var onSelected = new LayoutableEvent(delegate(ILayoutable sender)
            {
                var btn = sender as Button;
                if (btn != null)
                {
                    btn.Text = "Selected";
                    //InstructionManager.MoveToAccurate(SpriteManager.Camera, sender.X, sender.Y, SpriteManager.Camera.Z, 0.25);
                    btn.ResizeAroundText(20, 10);
                }
            });

            var onUnSelected = new LayoutableEvent(delegate(ILayoutable sender)
            {
                var btn = sender as Button;
                if (btn != null)
                {
                    btn.Text = "Not Selected";
                    btn.ResizeAroundText(5, 5);
                }
            });

            var onPressed = new LayoutableEvent(delegate(ILayoutable sender)
            {
                var btn = sender as Button;
                if (btn != null)
                    btn.Text = "Pressed";
            });

            var onRelease = new LayoutableEvent(delegate(ILayoutable sender)
            {
                var btn = sender as Button;
                if (btn != null)
                    btn.Text = "Clicked";
            });

            var newBtn = UiControlManager.Instance.CreateControl<Button>();
            newBtn.Text = "Button";
            newBtn.ResizeAroundText(5, 5);

            newBtn.AnimationChains = GlobalContent.Button1;
            newBtn.StandardAnimationChainName = "Available";

            newBtn.OnFocused = onSelected;
            newBtn.OnFocusLost = onUnSelected;
            newBtn.OnPushed = onPressed;
            newBtn.OnClicked = onRelease;

            return newBtn;
        }
    }
}
