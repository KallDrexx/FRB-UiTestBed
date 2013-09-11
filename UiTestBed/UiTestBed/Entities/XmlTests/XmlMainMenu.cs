using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
using FrbUi.Controls;
using FrbUi.Layouts;
using FrbUi.SelectableGroupings;
using FrbUi.Xml;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

namespace UiTestBed.Entities.XmlTests
{
	public partial class XmlMainMenu
	{
	    private UserInterfacePackage _userInterface;
	    private SequentialSelectableGroup _buttonGroup;

		private void CustomInitialize()
		{
            _userInterface = new UserInterfacePackage("Content/Entities/XmlTests/XmlMainMenu/ui.xml", ContentManagerName);
		    var mainLayout = _userInterface.GetNamedControl<BoxLayout>("MenuLayout");
            mainLayout.AttachTo(this, false);

		    _buttonGroup = _userInterface.GetNamedSelectableGroup<SequentialSelectableGroup>("Buttons");

            // Setup events
		    _userInterface.GetNamedControl<Button>("LevelSelectButton").OnFocused = sender => OnFocused("LevelSelectArrow");
            _userInterface.GetNamedControl<Button>("OptionsButton").OnFocused = sender => OnFocused("OptionsArrow");
            _userInterface.GetNamedControl<Button>("QuitButton").OnFocused = sender => OnFocused("QuitArrow");

            _userInterface.GetNamedControl<Button>("LevelSelectButton").OnFocusLost = sender => OnFocusLost("LevelSelectArrow");
            _userInterface.GetNamedControl<Button>("OptionsButton").OnFocusLost = sender => OnFocusLost("OptionsArrow");
            _userInterface.GetNamedControl<Button>("QuitButton").OnFocusLost = sender => OnFocusLost("QuitArrow");
		}

		private void CustomActivity()
		{
            if (InputManager.Keyboard.KeyPushed(Keys.Down))
                _buttonGroup.FocusNextControl();
            else if (InputManager.Keyboard.KeyPushed(Keys.Up))
                _buttonGroup.FocusPreviousControl();
            else if (InputManager.Keyboard.KeyPushed(Keys.Enter))
                _buttonGroup.ClickFocusedControl();
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void OnFocused(string arrowName)
        {
            _userInterface.GetNamedControl<LayoutableSprite>(arrowName).Visible = true;
        }

        private void OnFocusLost(string arrowName)
        {
            _userInterface.GetNamedControl<LayoutableSprite>(arrowName).Visible = false;
        }
	}
}
