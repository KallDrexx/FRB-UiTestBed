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
using FrbUi.Layouts;
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

		private void CustomInitialize()
		{
            _userInterface = new UserInterfacePackage("Content/Entities/XmlTests/XmlMainMenu/ui.xml", ContentManagerName);
		    var mainLayout = _userInterface.GetNamedControl<BoxLayout>("MenuLayout");
            mainLayout.AttachTo(this, false);
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
