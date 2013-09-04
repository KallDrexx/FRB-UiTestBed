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
using FrbUi.Controls;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

namespace UiTestBed.Entities.Games.RpgDemo
{
	public partial class HealthIndicator
	{
	    private ProgressIndicator _progress;

		private void CustomInitialize()
		{
		    _progress = UiControlManager.Instance.CreateControl<ProgressIndicator>();

		    _progress.BackgroundAnimationChains = Animations;
		    _progress.IndicatorAnimationChains = Animations;
		    _progress.CurrentBackgroundAnimationChainName = "Background";
		    _progress.CurrentIndicatorAnimationChainName = "Indicator";

		    _progress.Value = 50;
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
