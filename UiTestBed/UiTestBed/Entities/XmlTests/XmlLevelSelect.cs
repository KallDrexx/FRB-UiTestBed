using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
using FrbUi;
using FrbUi.Layouts;
using FrbUi.SelectableGroupings;
using UiTestBed.Entities.XuiLikeDemo;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using FrbUi.Xml;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

namespace UiTestBed.Entities.XmlTests
{
	public partial class XmlLevelSelect
	{
        private UserInterfacePackage _userInterface;
	    private SequentialSelectableGroup _worldSelectGroup;
	    private GridSelectableGroup _levelSelectGroup;
	    private bool _inTransition;

		private void CustomInitialize()
		{
            _userInterface = new UserInterfacePackage("Content/Entities/XmlTests/XmlLevelSelect/ui.xml", ContentManagerName);
            var mainLayout = _userInterface.GetNamedControl<BoxLayout>("OverallLayout");
            mainLayout.AttachTo(this, false);

		    _worldSelectGroup = _userInterface.GetNamedSelectableGroup<SequentialSelectableGroup>("WorldSelectGroup");

            // Setup events
		    foreach (var grid in mainLayout.Items.OfType<GridLayout>())
		    {
		        grid.OnFocused += OnGridFocused;
		        grid.OnClicked += OnGridClicked;
		    }
		}

	    private void CustomActivity()
	    {
	        if (_inTransition)
	            return;

            if (_levelSelectGroup == null)
            {
                if (InputManager.Keyboard.KeyPushed(Keys.Right))
                    _worldSelectGroup.FocusNextControl();
                else if (InputManager.Keyboard.KeyPushed(Keys.Left))
                    _worldSelectGroup.FocusPreviousControl();
                else if (InputManager.Keyboard.KeyPushed(Keys.Enter))
                    _worldSelectGroup.ClickFocusedControl();
            }
            else
            {
                if (InputManager.Keyboard.KeyPushed(Keys.Right))
                    _levelSelectGroup.FocusNextControlInRow();
                else if (InputManager.Keyboard.KeyPushed(Keys.Left))
                    _levelSelectGroup.FocusPreviousControlInRow();
                else if (InputManager.Keyboard.KeyPushed(Keys.Down))
                    _levelSelectGroup.FocusNextControlInColumn();
                else if (InputManager.Keyboard.KeyPushed(Keys.Up))
                    _levelSelectGroup.FocusPreviousControlInColumn();
                else if (InputManager.Keyboard.KeyPushed(Keys.Escape))
                {
                    _levelSelectGroup.UnfocusCurrentControl();
                    _levelSelectGroup = null;
                }
            }
	    }

		private void CustomDestroy()
		{
            
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {
            
        }

        private void OnGridFocused(ILayoutable sender)
        {
            const double cameraMoveTime = 0.25;

            // Center the camera to the selected grid
            _inTransition = true;
            InstructionManager.MoveToAccurate(SpriteManager.Camera, sender.X, sender.Y, SpriteManager.Camera.Z, cameraMoveTime);
            this.Call(() => _inTransition = false).After(cameraMoveTime);
        }

        private void OnGridClicked(ILayoutable sender)
        {
            var grid = sender as GridLayout;
            if (grid == null)
                return;

            _levelSelectGroup = grid.GenerateGridSelectableGroup();
            _levelSelectGroup.LoopFocus = true;
            _levelSelectGroup.FocusNextControlInColumn();
        }
	}
}
