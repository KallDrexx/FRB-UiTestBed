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
using FrbUi.Layouts;
using FrbUi;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class LoadingScreen
	{
	    private BoxLayout _layout;
	    private LayoutableText _text;
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
		    _layout = UiControlManager.Instance.CreateControl<BoxLayout>();
		    _layout.CurrentDirection = BoxLayout.Direction.Down;
		    _layout.Spacing = 20;
		    _layout.Margin = 20;
            _layout.BackgroundAnimationChains = GlobalContent.MenuBackground;
            _layout.CurrentBackgroundAnimationChainName = "Idle";
            _layout.AttachTo(this, false);

            // Add the text and the loading image
            _text = UiControlManager.Instance.CreateControl<LayoutableText>();
            _text.DisplayText = "Loading Level";
            _layout.AddItem(_text, BoxLayout.Alignment.Centered);

		    var img = UiControlManager.Instance.CreateControl<LayoutableSprite>();
		    img.AnimationChains = GlobalContent.LoadingAnimation;
		    img.CurrentAnimationChainName = "Loading";
		    _layout.AddItem(img, BoxLayout.Alignment.Centered);
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
