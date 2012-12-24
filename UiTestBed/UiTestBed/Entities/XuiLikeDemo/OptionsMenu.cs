using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
using FlatRedBall.Instructions;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FrbUi.Layouts;
using FrbUi;
using FrbUi.Controls;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class OptionsMenu
	{
	    private const int MAX_VOLUME_LEVELS = 10;
	    private const float MAX_BAR_HEIGHT = 15;

        private BoxLayout _mainLayout;
	    private SelectableControlGroup _selectGroup;
	    private List<LayoutableSprite> _volumeBars;
	    private Button _difficultyButton;
        private LayoutableText _difficultyText;
	    private Button _volumeButton;
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
                _selectGroup.FocusNextControl();
                if (activationCallback != null)
                    activationCallback();
            }).After(SecondsToFade);
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
                MenuExited = true;
                if (deactivationCallback != null)
                    deactivationCallback();
            }).After(SecondsToFade);

            _selectGroup.UnfocusCurrentControl();
        }

		private void CustomInitialize()
		{
            _volumeBars = new List<LayoutableSprite>();
		    InitLayouts();
		}

	    private void CustomActivity()
		{
            if (IsActive)
            {
                if (InputManager.Keyboard.KeyPushed(Keys.Down))
                    _selectGroup.FocusNextControl();
                else if (InputManager.Keyboard.KeyPushed(Keys.Up))
                    _selectGroup.FocusPreviousControl();
                else if (InputManager.Keyboard.KeyPushed(Keys.Enter))
                    _selectGroup.ClickFocusedControl();
                else if (InputManager.Keyboard.KeyPushed(Keys.Right))
                    ProcessOptionKeyPress(true);
                else if (InputManager.Keyboard.KeyPushed(Keys.Left))
                    ProcessOptionKeyPress(false);
            }
		}

		private void CustomDestroy()
		{
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {

        }

        private void ProcessOptionKeyPress(bool increasing)
        {
            if (_difficultyButton.CurrentSelectableState == SelectableState.Focused)
            {
                ProcessDifficultyChange(increasing);
            }
            else if (_volumeButton.CurrentSelectableState == SelectableState.Focused)
            {
                VolumeLevel = (increasing ? VolumeLevel + 1 : VolumeLevel - 1);
                if (VolumeLevel < 1)
                    VolumeLevel = 1;
                else if (VolumeLevel > MAX_VOLUME_LEVELS)
                    VolumeLevel = MAX_VOLUME_LEVELS;

                // Refresh the bars
                for (int x = 0; x < _volumeBars.Count; x++)
                    _volumeBars[x].Visible = (x < VolumeLevel);
            }
        }

	    private void ProcessDifficultyChange(bool increasing)
	    {
            // Find the current value and go to the next or previous depending on if increasing
	        var values = Enum.GetValues(typeof (Difficulty)).Cast<Difficulty>().ToArray();

	        int index = 0;
	        for (int x = 0; x < values.Length; x++)
	        {
	            if (values[x] == CurrentDifficultyState)
	            {
	                index = x;
	                break;
	            }
	        }

	        // Change the indexes until we are on the next/previous enum value that is not
	        //  Uninitialized or unknown
	        do
	        {
	            index = (increasing ? index + 1 : index - 1);
	            if (index >= values.Length)
	                index = 0;
	            else if (index < 0)
	                index = values.Length - 1;
	        } while (values[index] == Difficulty.Uninitialized || values[index] == Difficulty.Unknown);

	        CurrentDifficultyState = values[index];
	        _difficultyText.DisplayText = values[index].ToString();
	    }

	    private void InitLayouts()
        {
            _selectGroup = UiControlManager.Instance.CreateSelectableControlGroup();

            _mainLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            _mainLayout.CurrentDirection = BoxLayout.Direction.Down;
            _mainLayout.Spacing = 20;
            _mainLayout.Alpha = OverallAlpha;

            // Create the inner layouts and buttons
            CreateDifficultySection();
            CreateVolumeSection();
            CreateExitSection();
        }

	    private void CreateDifficultySection()
	    {
	        var difficultyLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
	        difficultyLayout.CurrentDirection = BoxLayout.Direction.Right;
	        difficultyLayout.Spacing = 20;
	        _mainLayout.AddItem(difficultyLayout);

            _difficultyButton = CreateButton();
	        _difficultyButton.Text = "Difficulty";
	        _selectGroup.Add(_difficultyButton);
	        difficultyLayout.AddItem(_difficultyButton);

	        var arrow1 = CreateArrow();
	        difficultyLayout.AddItem(arrow1);
	        arrow1.RelativeRotationZ = (float) (Math.PI*1.5);
	        arrow1.Visible = false;

            _difficultyText = UiControlManager.Instance.CreateControl<LayoutableText>();
	        _difficultyText.DisplayText = CurrentDifficultyState.ToString();
	        difficultyLayout.AddItem(_difficultyText);

	        var arrow2 = CreateArrow();
	        difficultyLayout.AddItem(arrow2);
	        arrow2.RelativeRotationZ = (float) (Math.PI*0.5);
	        arrow2.Visible = false;

	        // Set up the button events
	        _difficultyButton.OnFocused += sender =>
	            {
	                arrow1.Visible = true;
	                arrow2.Visible = true;
	            };

	        _difficultyButton.OnFocusLost += sender =>
	            {
	                arrow1.Visible = false;
	                arrow2.Visible = false;
	            };
	    }

        private void CreateVolumeSection()
        {
            var volumeLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            volumeLayout.CurrentDirection = BoxLayout.Direction.Right;
            volumeLayout.Spacing = 20;
            _mainLayout.AddItem(volumeLayout);

            _volumeButton = CreateButton();
            _volumeButton.Text = "Volume";
            _selectGroup.Add(_volumeButton);
            volumeLayout.AddItem(_volumeButton);

            var arrow1 = CreateArrow();
            volumeLayout.AddItem(arrow1);
            arrow1.RelativeRotationZ = (float)(Math.PI * 1.5);
            arrow1.Visible = false;

            // Create the volume bars
            var barLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            barLayout.CurrentDirection = BoxLayout.Direction.Right;
            volumeLayout.AddItem(barLayout);
            _volumeBars.Clear();

            for (int x = 0; x < MAX_VOLUME_LEVELS; x++)
            {
                var bar = UiControlManager.Instance.CreateControl<LayoutableSprite>();
                bar.AnimationChains = GlobalContent.MenuVolumeBar;
                bar.CurrentAnimationChainName = "Idle";
                //bar.Visible = (VolumeLevel > x);
                
                // Scale the bar vertical size to be proportional to the total
                var percent = ((float)(x + 1)/ (float)MAX_VOLUME_LEVELS);
                bar.ScaleY = (percent * MAX_BAR_HEIGHT);

                barLayout.AddItem(bar, true);
                _volumeBars.Add(bar);
            }

            var arrow2 = CreateArrow();
            volumeLayout.AddItem(arrow2);
            arrow2.RelativeRotationZ = (float)(Math.PI * 0.5);
            arrow2.Visible = false;

            // Set up the button events
            _volumeButton.OnFocused += sender =>
            {
                arrow1.Visible = true;
                arrow2.Visible = true;
            };

            _volumeButton.OnFocusLost += sender =>
            {
                arrow1.Visible = false;
                arrow2.Visible = false;
            };
        }

	    private void CreateExitSection()
	    {
	        var btn = CreateButton();
	        btn.Text = "Main Menu";
            _mainLayout.AddItem(btn);
            _selectGroup.Add(btn);

	        btn.OnClicked = sender => Deactivate();
	    }

	    private static Button CreateButton()
	    {
	        var btn = UiControlManager.Instance.CreateControl<Button>();
	        btn.AnimationChains = GlobalContent.MenuButtonAnimations;
	        btn.StandardAnimationChainName = "Idle";
	        btn.FocusedAnimationChainName = "Selected";
	        btn.ScaleX = 100;
	        btn.ScaleY = 20;
	        btn.IgnoreCursorEvents = true;
	        return btn;
	    }

	    private static LayoutableSprite CreateArrow()
	    {
	        var arrow = UiControlManager.Instance.CreateControl<LayoutableSprite>();
	        arrow.AnimationChains = GlobalContent.MenuArrow;
	        arrow.CurrentAnimationChainName = "Idle";
	        return arrow;
	    }
	}
}