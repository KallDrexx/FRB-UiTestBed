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

#if FRB_XNA || SILVERLIGHT
using FrbUi;
using FrbUi.Controls;
using FrbUi.Layouts;
using FrbUi.Positioning;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

namespace UiTestBed.Screens
{
	public partial class CenteredBoxLayoutTest
	{
        private SimpleLayout _mainLayout;

		void CustomInitialize()
		{
		    var rand = new Random();
            _mainLayout = UiControlManager.Instance.CreateControl<SimpleLayout>();

		    var outsideBox = UiControlManager.Instance.CreateControl<BoxLayout>();
            outsideBox.CurrentDirection = BoxLayout.Direction.Right;
		    outsideBox.Spacing = 15;

            for (var x = 0; x < 10; x++)
            {
                var layout = UiControlManager.Instance.CreateControl<BoxLayout>();
                layout.CurrentDirection = BoxLayout.Direction.Down;
                layout.Spacing = 10;

                outsideBox.AddItem(layout, BoxLayout.Alignment.Centered);

                var count = rand.Next(5, 10);
                for (var y = 0; y < x; y++)
                {
                    var btn = CreateButton();
                    btn.Text = "Button #" + y;
                    btn.ResizeAroundText(5, 5);
                    layout.AddItem(btn);
                }
            }

                _mainLayout.FullScreen = true;
            _mainLayout.AddItem(outsideBox, HorizontalPosition.PercentFromLeft(5), VerticalPosition.PercentFromTop(-5), LayoutOrigin.TopLeft);
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

        private static Button CreateButton()
        {
            var onSelected = new LayoutableEvent(sender =>
                {
                    var btn = sender as Button;
                    if (btn != null)
                    {
                        btn.Text = "Selected";
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

            var onRelease = new LayoutableEvent(sender =>
                {
                    var btn = sender as Button;
                    if (btn != null)
                        btn.Text = "Clicked";
                });

            var newBtn = UiControlManager.Instance.CreateControl<Button>();
            newBtn.AnimationChains = GlobalContent.Button1;
            newBtn.StandardAnimationChainName = "Available";

            newBtn.Text = "Button";
            newBtn.ResizeAroundText(5, 5);

            newBtn.OnFocused = onSelected;
            newBtn.OnFocusLost = onUnSelected;
            newBtn.OnPushed = onPressed;
            newBtn.OnClicked = onRelease;

            return newBtn;
        }
	}
}
