using System;
using FlatRedBall;
using FlatRedBall.Input;
#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using FrbUi.Layouts;
using FrbUi.Controls;
using FrbUi;

#endif
using FlatRedBall.Instructions;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class MainMenu
	{
	    private BoxLayout _layout;
	    private Button _levelSelectButton;
	    private Button _optionsButton;
	    private Button _quitButton;
	    private SelectableControlGroup _mainGroup;
	    private bool _activationStateChanging;

        public void Activate(Action activationCallback = null)
        {
            // If we are already active, or in the process of activation, ignore
            if (CurrentState == VariableState.Activated || _activationStateChanging)
                return;

            _activationStateChanging = true;
            InterpolateToState(VariableState.Activated, SecondsToFade);
            this.Call(() => 
                {
                    IsActive = true;
                    CurrentState = VariableState.Activated;
                    _activationStateChanging = false;
                    if (activationCallback != null)
                        activationCallback();
                })
                .After(SecondsToFade);
        }

        public void Deactivate(Action deactivationCallback = null)
        {
            // If we are already deactivated or in the process of activation/deactivation
            if (CurrentState == VariableState.Deactivated || _activationStateChanging)
                return;

            IsActive = false;
            InterpolateToState(VariableState.Deactivated, SecondsToFade);
            this.Call(() =>
                {
                    _activationStateChanging = false;
                    if (deactivationCallback != null)
                        deactivationCallback();
                })
                .After(SecondsToFade);
        }

		private void CustomInitialize()
		{
            _mainGroup = UiControlManager.Instance.CreateSelectableControlGroup();

            _layout = UiControlManager.Instance.CreateControl<BoxLayout>();
            _layout.CurrentDirection = BoxLayout.Direction.Down;
            _layout.Spacing = 40;
		    //_layout.BackgroundAnimationChains = GlobalContent.MenuBackground;
		    //_layout.CurrentBackgroundAnimationChainName = "Idle";

            _layout.AttachTo(this, false);

            InitButtons();
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

	    private void InitButtons()
        {
            _levelSelectButton = SetupButton("Select Level");
            _optionsButton = SetupButton("Options");
            _quitButton = SetupButton("Quit");

            _quitButton.OnClicked = sender => Deactivate(() => FlatRedBallServices.Game.Exit());
	        _optionsButton.OnClicked = sender => Deactivate(() => OptionsSelected = true);
        }

	    private Button SetupButton(string label)
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

	    private void ButtonFocused(ILayoutable sender)
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

	    private void ButtonLostFocused(ILayoutable sender)
        {
            ArrowSprite.Visible = false;
        }
	}
}
