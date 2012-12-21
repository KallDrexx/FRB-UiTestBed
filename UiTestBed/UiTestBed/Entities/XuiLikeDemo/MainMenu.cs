using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using FrbUi.Layouts;
using FrbUi.Controls;
using FrbUi;

#endif

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class MainMenu
	{
        protected BoxLayout _layout;
        protected Button _levelSelectButton;
        protected Button _optionsButton;
        protected Button _quitButton;
        protected SelectableControlGroup _mainGroup;
        protected double? _activationStartTime;
        protected bool _isActivating;
        protected Action _activationCallback;

        public void Activate(Action activationCallback = null)
        {
            // If we are already active, or in the process of activation, ignore
            if (IsActive || _activationStartTime != null)
                return;

            _activationStartTime = TimeManager.CurrentTime;
            _isActivating = true;
            _activationCallback = activationCallback;
        }

        public void Deactivate(Action deactivationCallback = null)
        {
            // If we are already deactivated or in the process of activation/deactivation
            if (!IsActive || _activationStartTime != null)
                return;

            _activationStartTime = TimeManager.CurrentTime;
            _isActivating = false;
            IsActive = false;
            _activationCallback = deactivationCallback;
            _mainGroup.UnfocusCurrentControl();
        }

		private void CustomInitialize()
		{
            _mainGroup = UiControlManager.Instance.CreateSelectableControlGroup();

            _layout = UiControlManager.Instance.CreateControl<BoxLayout>();
            _layout.CurrentDirection = BoxLayout.Direction.Down;
            _layout.Spacing = 40;

            _layout.AttachTo(this, false);

            InitButtons();
		}

		private void CustomActivity()
		{
            if (_activationStartTime != null)
            {
                double pctDone = (TimeManager.CurrentTime - _activationStartTime.Value) / SecondsToFade;
                if (pctDone >= 1)
                {
                    _activationStartTime = null;
                    if (_isActivating)
                        IsActive = true;

                    if (_activationCallback != null)
                    {
                        _activationCallback();
                        _activationCallback = null;
                    }
                }

                if (_isActivating)
                    Alpha = (float)pctDone;
                else
                    Alpha = (float)Math.Max(0, (1 - pctDone));
            }

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

            _quitButton.OnClicked += delegate(ILayoutable sender)  { Deactivate(() => FlatRedBallServices.Game.Exit()); };
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
            btn.OnFocused = ButtonFocused;
            btn.OnFocusLost = ButtonLostFocused;
            _mainGroup.Add(btn);
            _layout.AddItem(btn);

            return btn;
        }

        protected void ButtonFocused(ILayoutable sender)
        {
            const float SPACING = 25;

            ArrowSprite.RelativeY = sender.RelativeY;
            ArrowSprite.Visible = true;

            if (sender != _optionsButton)
            {
                ArrowSprite.RelativeX = (sender.RelativeX - sender.ScaleX - SPACING);
                ArrowSprite.RelativeRotationZ = (float)(Math.PI * 0.5);
            }
            else
            {
                ArrowSprite.RelativeX = (sender.RelativeX + sender.ScaleX + SPACING);
                ArrowSprite.RelativeRotationZ = (float)(Math.PI * 1.5);
            }
        }

        protected void ButtonLostFocused(ILayoutable sender)
        {
            ArrowSprite.Visible = false;
        }
	}
}
