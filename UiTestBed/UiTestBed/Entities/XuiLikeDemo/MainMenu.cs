using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using FrbUi.Layouts;
using FrbUi.Controls;
using FrbUi;

#endif

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class MainMenu
	{
        protected BoxLayout _layout;
        protected Button _levelSelectButton;
        protected Button _optionsButton;
        protected Button _quitButton;
        protected SelectableControlGroup _mainGroup;

		private void CustomInitialize()
		{
            _mainGroup = UiControlManager.Instance.CreateSelectableControlGroup();

            _layout = UiControlManager.Instance.CreateControl<BoxLayout>();
            _layout.CurrentDirection = BoxLayout.Direction.Down;
            _layout.Spacing = 40;

            _layout.AttachTo(this, false);

            InitButtons();

            _mainGroup.FocusNextControl();
		}

		private void CustomActivity()
		{
            if (IsActive)
            {
                if (InputManager.Keyboard.KeyPushed(Keys.Down))
                    _mainGroup.FocusNextControl();
                else if (InputManager.Keyboard.KeyPushed(Keys.Up))
                    _mainGroup.FocusPreviousControl();
                else if (InputManager.Keyboard.KeyPushed(Keys.Enter))
                    _mainGroup.ClickFocusedControl();
            }
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        protected void InitButtons()
        {
            _levelSelectButton = SetupButton("Select Level");
            _optionsButton = SetupButton("Options");
            _quitButton = SetupButton("Quit");

            _quitButton.OnClicked += delegate(ILayoutable sender) { FlatRedBallServices.Game.Exit(); };
        }

        protected Button SetupButton(string label)
        {
            var btn = UiControlManager.Instance.CreateControl<Button>();
            btn.AnimationChains = GlobalContent.MenuButtonAnimations;
            btn.StandardAnimationChainName = "Idle";
            btn.FocusedAnimationChainName = "Selected";
            btn.Text = label;
            btn.ScaleX = 100;
            btn.ScaleY = 19.7f;
            btn.IgnoreCursorEvents = true;
            _mainGroup.Add(btn);
            _layout.AddItem(btn);

            return btn;
        }

        protected void ButtonFocused(ILayoutable sender)
        {
        }
	}
}
