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
using UiTestBed.Entities.Layouts;
using UiTestBed.Data;
using FlatRedBall.Graphics;

namespace UiTestBed.Screens
{
	public partial class ButtonTestScreen1
	{
		void CustomInitialize()
		{
            var outterLayout = new BoxLayoutManager(ContentManagerName);
            outterLayout.CurrentDirectionState = BoxLayoutManager.Direction.Down;
            outterLayout.Spacing = 5;
            BoxLayouts.Add(outterLayout);

            var firstLayout = new BoxLayoutManager(ContentManagerName);
            firstLayout.CurrentDirectionState = BoxLayoutManager.Direction.Right;
            firstLayout.Spacing = 3;
            firstLayout.Margin = 5;
            BoxLayouts.Add(firstLayout);
            outterLayout.AddItem(firstLayout);

            for (int x = 0; x < 5; x++)
            {
                var btn = CreateButton();
                btn.Text = "Button # " + x;
                btn.ResizeAroundText(5, 5);
                firstLayout.AddItem(btn);
            }

            var circleLayout = new CircularLayoutManager(ContentManagerName);
            circleLayout.Radius = 100;
            circleLayout.Margin = 0;
            circleLayout.StartingDegrees = 90;
            circleLayout.MinDegreeOffset = 45;
            circleLayout.CurrentArrangementModeState = CircularLayoutManager.ArrangementMode.EvenlySpaced;
            CircularLayouts.Add(circleLayout);
            outterLayout.AddItem(circleLayout);

            for (int x = 0; x < 5; x++)
            {
                var btn = CreateButton();
                btn.Text = "Button # " + x;
                btn.ResizeAroundText(5, 5);
                circleLayout.Add(btn);
            }

            var secondLayout = new BoxLayoutManager(ContentManagerName);
            secondLayout.CurrentDirectionState = BoxLayoutManager.Direction.Right;
            secondLayout.Spacing = 3;
            secondLayout.Margin = 5;
            BoxLayouts.Add(secondLayout);
            outterLayout.AddItem(secondLayout);

            for (int x = 0; x < 5; x++)
            {
                var btn = CreateButton();
                btn.Text = "Button # " + x;
                btn.ResizeAroundText(5, 5);
                secondLayout.AddItem(btn);
            }

            MainLayout.FullScreen = true;
            MainLayout.AddItem(outterLayout, HorizontalPosition.PercentFromLeft(5), VerticalPosition.PercentFromTop(-5), LayoutOrigin.TopLeft);
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

        private UiButton CreateButton()
        {
            var onSelected = new EventHandler(delegate(object sender, EventArgs e)
            {
                var btn = sender as UiButton;
                if (btn != null)
                {
                    btn.Text = "Selected";
                    //InstructionManager.MoveToAccurate(SpriteManager.Camera, sender.X, sender.Y, SpriteManager.Camera.Z, 0.25);
                    btn.ResizeAroundText(20, 10);
                }
            });

            var onUnSelected = new EventHandler(delegate(object sender, EventArgs e)
            {
                var btn = sender as UiButton;
                if (btn != null)
                {
                    btn.Text = "Not Selected";
                    btn.ResizeAroundText(5, 5);
                }
            });

            var onPressed = new EventHandler(delegate(object sender, EventArgs e)
            {
                var btn = sender as UiButton;
                if (btn != null)
                    btn.Text = "Pressed";
            });

            var onRelease = new EventHandler(delegate(object sender, EventArgs e)
            {
                var btn = sender as UiButton;
                if (btn != null)
                    btn.Text = "Released";
            });

            var newBtn = new UiButton(ContentManagerName, false);
            newBtn.AddToManagers(UiLayer);
            newBtn.SpriteScaleX = newBtn.SpriteTexture.Width * newBtn.SpritePixelSize;
            newBtn.SpriteScaleY = newBtn.SpriteTexture.Height * newBtn.SpritePixelSize;

            newBtn.OnSelectedHandler += onSelected;
            newBtn.OnUnSelectedHandler += onUnSelected;
            newBtn.OnPressedHandler += onPressed;
            newBtn.OnReleasedHandler += onRelease;

            Buttons.Add(newBtn);
            return newBtn;
        }
	}
}
