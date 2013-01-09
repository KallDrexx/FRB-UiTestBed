using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
using FlatRedBall.Instructions;
using FrbUi;
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
using FrbUi.Layouts;
using FrbUi.Controls;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class LevelSelectMenu
	{
	    private BoxLayout _mainLayout;
        private SequentialSelectableGroup _mainGroup;
	    private Dictionary<GridLayout, GridSelectableGroup> _gridSelectableGroups;
	    private bool _gridSelected;
	    private bool _inTransition;
        private bool _activationStateChanging;

        public void Activate(Action activationCallback = null)
        {
            // If we are already active, or in the process of activation, ignore
            if (CurrentState != VariableState.Inactive || _activationStateChanging)
                return;

            _activationStateChanging = true;
            _mainGroup.FocusNextControl();
            InterpolateToState(VariableState.WorldUnSelected, SecondsToFade);
            this.Call(() =>
            {
                IsActive = true;
                _activationStateChanging = false;

                if (activationCallback != null)
                    activationCallback();
            })
                .After(SecondsToFade);
        }

        public void Deactivate(Action deactivationCallback = null)
        {
            // If we are already deactivated or in the process of activation/deactivation
            if (CurrentState == VariableState.Inactive || _activationStateChanging)
                return;

            IsActive = false;
            InterpolateToState(VariableState.Inactive, SecondsToFade);
            _mainGroup.UnfocusCurrentControl();

            this.Call(() =>
            {
                _activationStateChanging = false;
                MenuExited = true;
                
                // Move the camera back to 0,0 for the main UI
                SpriteManager.Camera.X = 0;
                SpriteManager.Camera.Y = 0;

                if (deactivationCallback != null)
                    deactivationCallback();
            })
                .After(SecondsToFade);
        }

		private void CustomInitialize()
		{
		    _gridSelectableGroups = new Dictionary<GridLayout, GridSelectableGroup>();

		    _mainGroup = UiControlManager.Instance.CreateSelectableControlGroup<SequentialSelectableGroup>();

		    _mainLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            _mainLayout.CurrentDirection = BoxLayout.Direction.Right;
		    _mainLayout.Spacing = 100;

		    InitGrids();
		}

		private void CustomActivity()
		{
            if (_inTransition || !IsActive || _activationStateChanging)
                return;

            if (_gridSelected)
            {
                var currentGrid = _mainGroup.CurrentlyFocusedItem as GridLayout;

                if (currentGrid != null && _gridSelectableGroups.ContainsKey(currentGrid))
                {
                    if (InputManager.Keyboard.KeyPushed(Keys.Up))
                        _gridSelectableGroups[currentGrid].FocusPreviousControlInColumn();
                    else if (InputManager.Keyboard.KeyPushed(Keys.Down))
                        _gridSelectableGroups[currentGrid].FocusNextControlInColumn();
                    else if (InputManager.Keyboard.KeyPushed(Keys.Right))
                        _gridSelectableGroups[currentGrid].FocusNextControlInRow();
                    else if (InputManager.Keyboard.KeyPushed(Keys.Left))
                        _gridSelectableGroups[currentGrid].FocusPreviousControlInRow();
                    else if (InputManager.Keyboard.KeyPushed(Keys.Enter))
                        _gridSelectableGroups[currentGrid].ClickFocusedControl();

                    else if (InputManager.Keyboard.KeyPushed(Keys.Escape))
                    {
                        _inTransition = true;
                        _gridSelected = false;

                        const double transitionTime = 0.25;
                        _gridSelectableGroups[currentGrid].UnfocusCurrentControl();
                        InterpolateToState(VariableState.WorldUnSelected, transitionTime);
                        this.Call(() => _inTransition = false).After(transitionTime);
                    }
                }
            }
            else
            {
                if (InputManager.Keyboard.KeyPushed(Keys.Right))
                    _mainGroup.FocusNextControl();
                else if (InputManager.Keyboard.KeyPushed(Keys.Left))
                    _mainGroup.FocusPreviousControl();
                else if (InputManager.Keyboard.KeyPushed(Keys.Enter))
                    _mainGroup.ClickFocusedControl();

                else if (InputManager.Keyboard.KeyPushed(Keys.Escape))
                    Deactivate();
            }
		}

		private void CustomDestroy()
		{
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {
        }

        private void InitGrids()
        {
            for (int x = 1; x < 5; x++)
                CreateGridLayout(x);
        }

	    private void CreateGridLayout(int world)
	    {
	        var grid = UiControlManager.Instance.CreateControl<GridLayout>();
	        var gridGroup = UiControlManager.Instance.CreateSelectableControlGroup<GridSelectableGroup>();

	        _gridSelectableGroups.Add(grid, gridGroup);
            _mainGroup.Add(grid);
            _mainLayout.AddItem(grid);

	        const int rowcount = 5;
	        const int columnCount = 5;
	        for (int x = 0; x < rowcount; x++)
	        {
	            for (int y = 0; y < columnCount; y++)
	            {
	                var levelNum = (x*rowcount) + (y + 1);
	                var btn = CreateLevelButton(string.Concat(world, "-", levelNum));

	                grid.AddItem(btn, x, y);
	                gridGroup.AddItem(btn, x, y);
	            }
	        }

            // Add the grid events
	        grid.OnFocused = sender =>
	        {
	            const double cameraMoveTime = 0.25;

                // Center the camera to the selected grid
                _inTransition = true;
                InstructionManager.MoveToAccurate(SpriteManager.Camera, sender.X, sender.Y, SpriteManager.Camera.Z, cameraMoveTime);
	            this.Call(() => _inTransition = false).After(cameraMoveTime);
	        };

	        grid.OnClicked = sender =>
	        {
	            const double transitionTime = 0.25;
                _inTransition = true;

	            InterpolateToState(VariableState.WorldSelected, transitionTime);
	            this.Call(() =>
	            {
	                _inTransition = false;
                    _gridSelected = true;
	                gridGroup.FocusNextControlInRow();
	            }).After(transitionTime);
	        };
	    }

	    private Button CreateLevelButton(string levelTitle)
        {
            var btn = UiControlManager.Instance.CreateControl<Button>();
            btn.AnimationChains = GlobalContent.MenuButtonAnimations;
            btn.StandardAnimationChainName = "Idle";
            btn.FocusedAnimationChainName = "Selected";
            btn.Text = levelTitle;
            btn.ScaleX = 50;
            btn.ScaleY = 50;
            btn.IgnoreCursorEvents = true;

            btn.OnFocused = sender => btn.Text = "Play " + levelTitle;
            btn.OnFocusLost = sender => btn.Text = levelTitle;
	        btn.OnClicked = sender =>
	        {
	            LevelToLoad = levelTitle;
	            Deactivate(() => MenuExited = true );
	        };

            return btn;
        }
	}
}
