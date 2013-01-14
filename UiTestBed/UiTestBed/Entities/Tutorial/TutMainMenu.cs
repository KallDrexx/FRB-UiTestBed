using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using FrbUi;
using FrbUi.Controls;
using FrbUi.Layouts;
using FrbUi.SelectableGroupings;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

namespace UiTestBed.Entities.Tutorial
{
	public partial class TutMainMenu
	{
        private BoxLayout _layout;
        private Button _levelSelectButton;
        private Button _optionsButton;
        private Button _quitButton;
        private SequentialSelectableGroup _mainGroup;
        private bool _activationStateChanging;
	    private bool _isActive;

        public void Activate(Action activationCallback = null)
        {
            // If we are already active, or in the process of activation, ignore
            if (CurrentState == VariableState.Activated || _activationStateChanging)
                return;

            // Interpolate to the activated state in the span of SecondsToFade
            _activationStateChanging = true;
            InterpolateToState(VariableState.Activated, SecondsToFade);
            this.Call(() =>
            {
                _isActive = true;
                _activationStateChanging = false;

                // Once activated, focus on the first control
                _mainGroup.FocusNextControl();

                if (activationCallback != null)
                    activationCallback();
            }).After(SecondsToFade);
        }

        public void Deactivate(Action deactivationCallback = null)
        {
            // If we are already deactivated or in the process of activation/deactivation
            if (CurrentState == VariableState.Deactivated || _activationStateChanging)
                return;

            // Immediately disable activation and unfocus the focused button
            _mainGroup.UnfocusCurrentControl();

            _isActive = false;
            InterpolateToState(VariableState.Deactivated, SecondsToFade);
            this.Call(() =>
            {
                _activationStateChanging = false;
                if (deactivationCallback != null)
                    deactivationCallback();
            }).After(SecondsToFade);
        }

		private void CustomInitialize()
		{
		    _mainGroup = UiControlManager.Instance.CreateSelectableControlGroup < SequentialSelectableGroup>();

            // Create the main layout
            _layout = UiControlManager.Instance.CreateControl<BoxLayout>();
            _layout.CurrentDirection = BoxLayout.Direction.Down;
            _layout.Spacing = 40;
            _layout.Margin = 50;

            // Hook up the animation chains for the layout
            _layout.BackgroundAnimationChains = GlobalContent.MenuBackground;
            _layout.CurrentBackgroundAnimationChainName = "Idle";

            // Attach the layout to this entity so if we move the entity it moves the layout as well
            _layout.AttachTo(this, false);

		    InitButtons();
		    SpriteManager.AddToLayer(MenuArrow, UiControlManager.Instance.Layer);

		    _layout.Alpha = OverallAlpha;
            MenuArrow.Visible = false;
		}

		private void CustomActivity()
		{
            if (_isActive)
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

        private void InitButtons()
        {
            _levelSelectButton = SetupButton("Select Level");
            _optionsButton = SetupButton("Options");
            _quitButton = SetupButton("Quit");

            _quitButton.OnClicked = sender => Deactivate(() => FlatRedBallServices.Game.Exit());
        }

        private Button SetupButton(string label)
        {
            // Instantiate the button and set the dimensions
            var btn = UiControlManager.Instance.CreateControl<Button>();
            btn.Text = label;
            btn.ScaleX = 100;
            btn.ScaleY = 19.7f;

            // Setup the button animation chains
            btn.AnimationChains = GlobalContent.MenuButtonAnimations;
            btn.StandardAnimationChainName = "Idle";
            btn.FocusedAnimationChainName = "Selected";
            
            // Make sure the button does not respond to mouse events
            btn.IgnoreCursorEvents = true;

            // Add the button to the box layout
            _layout.AddItem(btn);

            btn.OnFocused = ButtonFocused;
            btn.OnFocusLost = ButtonLostFocused;

            _mainGroup.Add(btn);

            return btn;
        }

        private void ButtonFocused(ILayoutable sender)
        {
            const float SPACING = 25;

            MenuArrow.RelativeY = sender.RelativeY;
            MenuArrow.Visible = true;

            if (sender != _optionsButton)
            {
                // To add some variety, if the option button is focused put the arrow on the opposite side
                MenuArrow.RelativeX = (sender.RelativeX - sender.ScaleX - SPACING);
                MenuArrow.RelativeRotationZ = (float)(Math.PI * 0.5);
            }
            else
            {
                MenuArrow.RelativeX = (sender.RelativeX + sender.ScaleX + SPACING);
                MenuArrow.RelativeRotationZ = (float)(Math.PI * 1.5);
            }
        }

        private void ButtonLostFocused(ILayoutable sender)
        {
            MenuArrow.Visible = false;
        }
	}
}
