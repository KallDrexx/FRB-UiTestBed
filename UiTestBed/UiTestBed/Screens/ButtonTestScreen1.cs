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
using FrbUi.LayoutManagers;

namespace UiTestBed.Screens
{
	public partial class ButtonTestScreen1
	{
        private SimpleLayoutManager _mainLayout;
        private UiSelectableControlGroup _topGroup;
        private UiSelectableControlGroup _circleGroup;
        private UiSelectableControlGroup _bottomGroup;

		void CustomInitialize()
		{
            _topGroup = UiControlManager.Instance.CreateSelectableControlGroup();
            _circleGroup = UiControlManager.Instance.CreateSelectableControlGroup();
            _bottomGroup = UiControlManager.Instance.CreateSelectableControlGroup();

            _mainLayout = new SimpleLayoutManager();
            UiControlManager.Instance.AddControl(_mainLayout);

            var grid = new GridLayoutManager();
            grid.Margin = 20;
            grid.Spacing = 30;
            UiControlManager.Instance.AddControl(grid);

            int count = 0;
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (col == 2 && row == 2)
                    {
                        var circle = new CircularLayoutManager();
                        circle.StartingDegrees = 90;
                        circle.Radius = 80;
                        circle.CurrentArrangementMode = CircularLayoutManager.ArrangementMode.EvenlySpaced;
                        circle.ShowBorder = false;
                        UiControlManager.Instance.AddControl(circle);

                        for (int x = 0; x < 5; x++)
                        {
                            var btn = CreateButton();
                            btn.Text = "#" + x;
                            btn.ResizeAroundText(5, 5);
                            circle.AddItem(btn);
                        }

                        grid.AddItem(circle, col, row, GridLayoutManager.HorizontalAlignment.Center, GridLayoutManager.VerticalAlignment.Center);
                    }
                    else
                    {
                        var btn = CreateButton();
                        btn.Text = string.Format("Button {0} - {1}", row, col);
                        btn.ResizeAroundText(10, 10);
                        grid.AddItem(btn, col, row, GridLayoutManager.HorizontalAlignment.Center, GridLayoutManager.VerticalAlignment.Center);
                    }
                }
            }

            _mainLayout.FullScreen = true;
            _mainLayout.AddItem(grid, HorizontalPosition.PercentFromLeft(5), VerticalPosition.PercentFromTop(-5), LayoutOrigin.TopLeft);
		}

        private void CreateButtonsForLayout(BoxLayoutManager layout)
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
            if (InputManager.Keyboard.KeyPushed(Keys.Right))
                _topGroup.FocusNextControl();
            else if (InputManager.Keyboard.KeyPushed(Keys.Left))
                _topGroup.FocusPreviousControl();

            else if (InputManager.Keyboard.KeyPushed(Keys.Up))
                _circleGroup.FocusNextControl();
            else if (InputManager.Keyboard.KeyPushed(Keys.Down))
                _circleGroup.FocusPreviousControl();

            else if (InputManager.Keyboard.KeyPushed(Keys.A))
                _bottomGroup.FocusNextControl();
            else if (InputManager.Keyboard.KeyPushed(Keys.Z))
                _bottomGroup.FocusPreviousControl();

            else if (InputManager.Keyboard.KeyPushed(Keys.D1))
                _topGroup.ClickFocusedControl();
            else if (InputManager.Keyboard.KeyPushed(Keys.D2))
                _circleGroup.ClickFocusedControl();
            else if (InputManager.Keyboard.KeyPushed(Keys.D3))
                _bottomGroup.ClickFocusedControl();
		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private Button CreateButton()
        {
            var onSelected = new ILayoutableEvent(delegate(ILayoutable sender)
            {
                var btn = sender as Button;
                if (btn != null)
                {
                    btn.Text = "Selected";
                    //InstructionManager.MoveToAccurate(SpriteManager.Camera, sender.X, sender.Y, SpriteManager.Camera.Z, 0.25);
                    btn.ResizeAroundText(20, 10);
                }
            });

            var onUnSelected = new ILayoutableEvent(delegate(ILayoutable sender)
            {
                var btn = sender as Button;
                if (btn != null)
                {
                    btn.Text = "Not Selected";
                    btn.ResizeAroundText(5, 5);
                }
            });

            var onPressed = new ILayoutableEvent(delegate(ILayoutable sender)
            {
                var btn = sender as Button;
                if (btn != null)
                    btn.Text = "Pressed";
            });

            var onRelease = new ILayoutableEvent(delegate(ILayoutable sender)
            {
                var btn = sender as Button;
                if (btn != null)
                    btn.Text = "Clicked";
            });

            var newBtn = new Button();
            newBtn.Text = "Button";
            newBtn.ResizeAroundText(5, 5);

            newBtn.AnimationChains = GlobalContent.Button1;
            newBtn.StandardAnimationChainName = "Available";

            newBtn.OnFocused = onSelected;
            newBtn.OnFocusLost = onUnSelected;
            newBtn.OnPushed = onPressed;
            newBtn.OnClicked = onRelease;

            UiControlManager.Instance.AddControl(newBtn);
            return newBtn;
        }
    }
}
