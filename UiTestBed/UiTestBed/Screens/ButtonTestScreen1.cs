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

		void CustomInitialize()
		{
            _mainLayout = new SimpleLayoutManager();
            UiControlManager.Instance.AddControl(_mainLayout);

            var outterLayout = new BoxLayoutManager();
            outterLayout.CurrentDirection = BoxLayoutManager.Direction.Down;
            outterLayout.Spacing = 5;
            UiControlManager.Instance.AddControl(outterLayout);

            var firstLayout = new BoxLayoutManager();
            firstLayout.CurrentDirection = BoxLayoutManager.Direction.Right;
            firstLayout.Spacing = 3;
            firstLayout.Margin = 5;

            UiControlManager.Instance.AddControl(firstLayout);
            outterLayout.AddItem(firstLayout);

            for (int x = 0; x < 5; x++)
            {
                var btn = CreateButton();
                btn.Text = "Button # " + x;
                btn.ResizeAroundText(5, 5);
                firstLayout.AddItem(btn);
            }

            var circleLayout = new CircularLayoutManager();
            circleLayout.Radius = 100;
            circleLayout.Margin = 0;
            circleLayout.StartingDegrees = 90;
            circleLayout.MinDegreeOffset = 45;
            circleLayout.CurrentArrangementMode = CircularLayoutManager.ArrangementMode.EvenlySpaced;

            UiControlManager.Instance.AddControl(circleLayout);
            outterLayout.AddItem(circleLayout);

            for (int x = 0; x < 5; x++)
            {
                var btn = CreateButton();
                btn.Text = "Button # " + x;
                btn.ResizeAroundText(5, 5);
                circleLayout.AddItem(btn);
            }

            var secondLayout = new BoxLayoutManager();
            secondLayout.CurrentDirection = BoxLayoutManager.Direction.Right;
            secondLayout.Spacing = 3;
            secondLayout.Margin = 5;

            UiControlManager.Instance.AddControl(secondLayout);
            outterLayout.AddItem(secondLayout);

            for (int x = 0; x < 5; x++)
            {
                var btn = CreateButton();
                btn.Text = "Button # " + x;
                btn.ResizeAroundText(5, 5);
                secondLayout.AddItem(btn);
            }

            _mainLayout.FullScreen = true;
            _mainLayout.AddItem(outterLayout, HorizontalPosition.PercentFromLeft(5), VerticalPosition.PercentFromTop(-5), LayoutOrigin.TopLeft);
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
            InputManager.Keyboard.ControlPositionedObject(SpriteManager.Camera);
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
                    btn.Text = "Released";
            });

            var newBtn = new Button();
            newBtn.Text = "Button";
            newBtn.ResizeAroundText(5, 5);

            newBtn.AnimationChains = GlobalContent.Button1;
            newBtn.CurrentChainName = "Available";

            newBtn.OnFocused = onSelected;
            newBtn.OnFocusLost = onUnSelected;
            newBtn.OnPushed = onPressed;
            newBtn.OnReleased = onRelease;

            UiControlManager.Instance.AddControl(newBtn);
            return newBtn;
        }
	}
}
