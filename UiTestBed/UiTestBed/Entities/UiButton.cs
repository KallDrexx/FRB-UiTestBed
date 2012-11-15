using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using UiTestBed.Entities.Interfaces;

namespace UiTestBed.Entities
{
	public partial class UiButton : ILayoutable
	{
        public event EventHandler OnSelectedHandler;
        public event EventHandler OnUnSelectedHandler;
        public event EventHandler OnPressedHandler;
        public event EventHandler OnReleasedHandler;

        public string Text
        {
            get { return TextInstance.DisplayText; }
            set { TextInstance.DisplayText = value; }
        }

        public float ScaleX
        {
            get
            {
                return SpriteFrameInstance.ScaleX;
            }
            set
            {
                // Scale the spriteframe, since that's the core of the button
                SpriteFrameInstance.ScaleX = value;
            }
        }

        public float ScaleXVelocity
        {
            get { return SpriteFrameInstance.ScaleXVelocity; } 
            set { SpriteFrameInstance.ScaleXVelocity = value; }
        }

        public float ScaleY
        {
            get { return SpriteFrameInstance.ScaleY; }
            set
            {
                // Scale the spriteframe, since that's the core of the button
                SpriteFrameInstance.ScaleY = value;
            }
        }

        public float ScaleYVelocity
        {
            get { return SpriteFrameInstance.ScaleYVelocity; }
            set { SpriteFrameInstance.ScaleYVelocity = value; }
        }

        public void Select()
        {
            if (CurrentState == VariableState.Idle)
            {
                CurrentState = VariableState.Selected;
                if (OnSelectedHandler != null)
                    OnSelectedHandler(this, new EventArgs());
            }
        }

        public void UnSelect()
        {
            if (CurrentState == VariableState.Selected)
            {
                CurrentState = VariableState.Idle;
                if (OnUnSelectedHandler != null)
                    OnUnSelectedHandler(this, new EventArgs());
            }
        }

        public void PressButton()
        {
            if (CurrentState == VariableState.Selected)
            {
                CurrentState = VariableState.Pressed;
                if (OnPressedHandler != null)
                    OnPressedHandler(this, new EventArgs());
            }
        }

        public void ReleaseButton()
        {
            if (CurrentState == VariableState.Pressed)
            {
                CurrentState = VariableState.Selected;
                if (OnReleasedHandler != null)
                    OnReleasedHandler(this, new EventArgs());
            }
        }

        public void PushButton()
        {
            PressButton();
            ReleaseButton();
        }

        public void ResizeAroundText(float horizontalMargin, float verticalMargin)
        {
            ScaleX = TextInstance.ScaleX + horizontalMargin;
            ScaleY = TextInstance.ScaleY + verticalMargin;
        }

		private void CustomInitialize()
		{
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
