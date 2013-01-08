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
using FrbUi;
using FrbUi.Controls;
using FrbUi.Layouts;
using FrbUi.Positioning;
using FrbUi.SelectableGroupings;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

namespace UiTestBed.Screens
{
	public partial class GridButtons
	{
        private SimpleLayout _mainLayout;
	    private GridSelectableGroup _group;

        void CustomInitialize()
        {
            _mainLayout = UiControlManager.Instance.CreateControl<SimpleLayout>();
            _group = UiControlManager.Instance.CreateSelectableControlGroup<GridSelectableGroup>();
            _group.LoopFocus = true;

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

                        grid.AddItem(circle, col, row, GridLayout.HorizontalAlignment.Center, GridLayout.VerticalAlignment.Center);
                    }
                    else
                    {
                        var btn = CreateButton();
                        btn.Text = string.Format("Button {0} - {1}", row, col);
                        btn.ResizeAroundText(10, 10);
                        grid.AddItem(btn, col, row, GridLayout.HorizontalAlignment.Center, GridLayout.VerticalAlignment.Center);
                        _group.AddItem(btn, row, col);
                    }
                }
            }

            _mainLayout.FullScreen = true;
            _mainLayout.AddItem(grid, HorizontalPosition.PercentFromLeft(5), VerticalPosition.PercentFromTop(-5), LayoutOrigin.TopLeft);
        }

        void CustomActivity(bool firstTimeCalled)
        {
            if (InputManager.Keyboard.KeyPushed(Keys.Right))
                _group.FocusNextControlInRow();
            else if (InputManager.Keyboard.KeyPushed(Keys.Left))
                _group.FocusPreviousControlInRow();
            else if (InputManager.Keyboard.KeyPushed(Keys.Up))
                _group.FocusPreviousControlInColumn();
            else if (InputManager.Keyboard.KeyPushed(Keys.Down))
                _group.FocusNextControlInColumn();
        }

        void CustomDestroy()
        {


        }

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private static Button CreateButton()
        {
            var onSelected = new LayoutableEvent(sender =>
            {
                var btn = sender as Button;
                if (btn != null)
                {
                    btn.Text = "Selected";
                    //InstructionManager.MoveToAccurate(SpriteManager.Camera, sender.X, sender.Y, SpriteManager.Camera.Z, 0.25);
                    btn.ResizeAroundText(20, 10);
                }
            });

            var onUnSelected = new LayoutableEvent(sender =>
            {
                var btn = sender as Button;
                if (btn != null)
                {
                    btn.Text = "Not Selected";
                    btn.ResizeAroundText(5, 5);
                }
            });

            var onPressed = new LayoutableEvent(sender =>
            {
                var btn = sender as Button;
                if (btn != null)
                    btn.Text = "Pressed";
            });

            var onClick = new LayoutableEvent(sender =>
            {
                var btn = sender as Button;
                if (btn != null)
                    btn.Text = "Clicked";
            });

            var newBtn = UiControlManager.Instance.CreateControl<Button>();
            newBtn.Text = "Button";
            newBtn.ResizeAroundText(5, 5);
            newBtn.IgnoreCursorEvents = true;

            newBtn.AnimationChains = GlobalContent.Button1;
            newBtn.StandardAnimationChainName = "Available";

            newBtn.OnFocused = onSelected;
            newBtn.OnFocusLost = onUnSelected;
            newBtn.OnPushed = onPressed;
            newBtn.OnClicked = onClick;

            return newBtn;
        }
	}
}
