using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
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

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class LevelSelectMenu
	{
	    private BoxLayout _mainLayout;
        private SequentialSelectableGroup _mainGroup;

		private void CustomInitialize()
		{
		    _mainGroup = UiControlManager.Instance.CreateSelectableControlGroup<SequentialSelectableGroup>();

		    _mainLayout = UiControlManager.Instance.CreateControl<BoxLayout>();
            _mainLayout.CurrentDirection = BoxLayout.Direction.Right;
		    _mainLayout.Spacing = 100;
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

        private void InitGrids()
        {
            
        }
	}
}
