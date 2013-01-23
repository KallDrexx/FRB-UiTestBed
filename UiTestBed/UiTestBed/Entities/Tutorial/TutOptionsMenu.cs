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
using FrbUi;
using FrbUi.Controls;
using FrbUi.Layouts;
using FrbUi.SelectableGroupings;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

namespace UiTestBed.Entities.Tutorial
{
	public partial class TutOptionsMenu
	{
        private BoxLayout _mainLayout;
        private Button _difficultyButton;
        private LayoutableText _difficultyText;
        private List<LayoutableSprite> _volumeBars;
        private Button _volumeButton;
	    private Button _exitButton;
        private SequentialSelectableGroup _selectGroup;

		private void CustomInitialize()
		{
		    _selectGroup = UiControlManager.Instance.CreateSelectableControlGroup<SequentialSelectableGroup>();

            _mainLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            _mainLayout.CurrentDirection = BoxLayout.Direction.Down;
            _mainLayout.Spacing = 20;
            _mainLayout.Margin = 40;
            _mainLayout.BackgroundAnimationChains = GlobalContent.MenuBackground;
            _mainLayout.CurrentBackgroundAnimationChainName = "Idle";

		    CreateDifficultySection();
		    CreateVolumeSection();
		    CreateExitSection();

            _selectGroup.Add(_difficultyButton);
            _selectGroup.Add(_volumeButton);
		    _selectGroup.Add(_exitButton);
		}

		private void CustomActivity()
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

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void CreateDifficultySection()
        {
            // Create the inner layout
            var difficultyLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            difficultyLayout.CurrentDirection = BoxLayout.Direction.Right;
            difficultyLayout.Spacing = 20;
            _mainLayout.AddItem(difficultyLayout);

            // Create the difficulty selection button
            _difficultyButton = CreateButton();
            _difficultyButton.Text = "Difficulty";
            difficultyLayout.AddItem(_difficultyButton, BoxLayout.Alignment.Centered);

            // Create the left arrow
            var arrow1 = CreateArrow();
            difficultyLayout.AddItem(arrow1, BoxLayout.Alignment.Centered);
            arrow1.RelativeRotationZ = (float)(Math.PI * 1.5);
            arrow1.Visible = false;

            // Create the difficulty text display
            _difficultyText = UiControlManager.Instance.CreateControl<LayoutableText>();
            _difficultyText.DisplayText = CurrentDifficultyState.ToString();
            difficultyLayout.AddItem(_difficultyText, BoxLayout.Alignment.Centered);

            // Create the right arrow
            var arrow2 = CreateArrow();
            difficultyLayout.AddItem(arrow2, BoxLayout.Alignment.Centered);
            arrow2.RelativeRotationZ = (float)(Math.PI * 0.5);
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
            _volumeBars = new List<LayoutableSprite>();

            var volumeLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            volumeLayout.CurrentDirection = BoxLayout.Direction.Right;
            volumeLayout.Spacing = 20;
            _mainLayout.AddItem(volumeLayout);

            _volumeButton = CreateButton();
            _volumeButton.Text = "Volume";
            volumeLayout.AddItem(_volumeButton, BoxLayout.Alignment.Centered);

            var arrow1 = CreateArrow();
            volumeLayout.AddItem(arrow1, BoxLayout.Alignment.Centered);
            arrow1.RelativeRotationZ = (float)(Math.PI * 1.5);
            arrow1.Visible = false;

            // Create the volume bars
            var barLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            barLayout.CurrentDirection = BoxLayout.Direction.Right;
            volumeLayout.AddItem(barLayout, BoxLayout.Alignment.Centered);
            _volumeBars.Clear();

            for (int x = 0; x < MaxVolumeLevel; x++)
            {
                var bar = UiControlManager.Instance.CreateControl<LayoutableSprite>();
                bar.AnimationChains = GlobalContent.MenuVolumeBar;
                bar.CurrentAnimationChainName = "Idle";
                bar.Visible = (VolumeLevel > x);

                // Scale the bar vertical size to be proportional to the total
                var percent = ((x + 1) / (float)MaxVolumeLevel);
                bar.ScaleY = (percent * MaxVolumeBarHeight);

                barLayout.AddItem(bar, BoxLayout.Alignment.Inverse);
                _volumeBars.Add(bar);
            }

            var arrow2 = CreateArrow();
            volumeLayout.AddItem(arrow2, BoxLayout.Alignment.Centered);
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
            _exitButton = CreateButton();
            _exitButton.Text = "Main Menu";
            _mainLayout.AddItem(_exitButton);

            //btn.OnClicked = sender => Deactivate();
        }

        private void ProcessOptionKeyPress(bool increasing)
        {
            if (_difficultyButton.CurrentSelectableState == SelectableState.Focused)
            {
                ProcessDifficultyChange(increasing);
            }
            else if (_volumeButton.CurrentSelectableState == SelectableState.Focused)
            {
                ProcessVolumeChange(increasing);
            }
        }

	    private void ProcessVolumeChange(bool increasing)
	    {
	        VolumeLevel = (increasing ? VolumeLevel + 1 : VolumeLevel - 1);
	        if (VolumeLevel < 1)
	            VolumeLevel = 1;
	        else if (VolumeLevel > MaxVolumeLevel)
	            VolumeLevel = MaxVolumeLevel;
	    }

        private void ProcessDifficultyChange(bool increasing)
        {
            // Find the current value and go to the next or previous depending on if increasing
            var values = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().ToArray();

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
