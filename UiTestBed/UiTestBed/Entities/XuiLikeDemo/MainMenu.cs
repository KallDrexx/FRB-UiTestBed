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
            _levelSelectButton = UiControlManager.Instance.CreateControl<Button>();
            _levelSelectButton.AnimationChains = GlobalContent.MenuButtonAnimations;
            _levelSelectButton.StandardAnimationChainName = "Idle";
            _levelSelectButton.FocusedAnimationChainName = "Selected";
            _levelSelectButton.Text = "Select Level";
            _levelSelectButton.ScaleX = 100;
            _levelSelectButton.ScaleY = 19.7f;
            _levelSelectButton.IgnoreCursorEvents = true;
            _mainGroup.Add(_levelSelectButton);
            _layout.AddItem(_levelSelectButton);

            _quitButton = UiControlManager.Instance.CreateControl<Button>();
            _quitButton.AnimationChains = GlobalContent.MenuButtonAnimations;
            _quitButton.StandardAnimationChainName = "Idle";
            _quitButton.FocusedAnimationChainName = "Selected";
            _quitButton.Text = "Quit";
            _quitButton.ScaleX = 100;
            _quitButton.ScaleY = 19.7f;
            _quitButton.IgnoreCursorEvents = true;
            _mainGroup.Add(_quitButton);
            _layout.AddItem(_quitButton);
        }

        protected void ButtonFocused(ILayoutable sender)
        {
        }
	}
}
