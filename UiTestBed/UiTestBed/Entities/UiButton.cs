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

namespace UiTestBed.Entities
{
    public delegate void ButtonEventHandler(UiButton sender);

    public enum ButtonDirection { Up, Down, Left, Right };

	public partial class UiButton
	{
        private Dictionary<ButtonDirection, UiButton> _adjacentButtons;
        private Dictionary<ButtonDirection, ButtonDirection> _oppositeDirections;
        private float _overallAlpha; 

        public string Text
        {
            get { return TextInstance.DisplayText; }
            set { TextInstance.DisplayText = value; }
        }

        public float Alpha
        {
            get { return _overallAlpha; }
            set
            {
                SpriteAlpha = value;
                TextInstanceAlpha = value;
                _overallAlpha = value;
            }
        }

        public event ButtonEventHandler OnSelectedHandler;
        public event ButtonEventHandler OnUnSelectedHandler;
        public event ButtonEventHandler OnPressedHandler;
        public event ButtonEventHandler OnReleasedHandler;

        public void Select()
        {
            if (CurrentState == VariableState.Idle)
            {
                CurrentState = VariableState.Selected;
                if (OnSelectedHandler != null)
                    OnSelectedHandler(this);
            }
        }

        public void UnSelect()
        {
            if (CurrentState == VariableState.Selected)
            {
                CurrentState = VariableState.Idle;
                if (OnUnSelectedHandler != null)
                    OnUnSelectedHandler(this);
            }
        }

        public void PressButton()
        {
            if (CurrentState == VariableState.Selected)
            {
                CurrentState = VariableState.Pressed;
                if (OnPressedHandler != null)
                    OnPressedHandler(this);
            }
        }

        public void ReleaseButton()
        {
            if (CurrentState == VariableState.Pressed)
            {
                CurrentState = VariableState.Selected;
                if (OnReleasedHandler != null)
                    OnReleasedHandler(this);
            }
        }

        public void PushButton()
        {
            PressButton();
            ReleaseButton();
        }

        public void SetAdjacentButton(UiButton button, ButtonDirection direction, bool setReverseDirection = true)
        {
            if (_adjacentButtons.ContainsKey(direction))
                _adjacentButtons[direction] = button;
            else
                _adjacentButtons.Add(direction, button);

            if (button != null && setReverseDirection)
                button.SetAdjacentButton(this, _oppositeDirections[direction], false);
                
        }

        public UiButton SelectAdjacentButton(ButtonDirection direction)
        {
            // Make sure a button is set as adjacent.  
            //   If none is adjacent in the specified direction, keep this button selected
            var btn = GetAdjacentButton(direction);
            if (btn == null)
                return this;

            this.UnSelect();
            btn.Select();
            return btn;
        }

        public UiButton GetAdjacentButton(ButtonDirection direction)
        {
            if (!_adjacentButtons.ContainsKey(direction) || _adjacentButtons[direction] == null)
                return null;

            return _adjacentButtons[direction];
        }

		private void CustomInitialize()
		{
            _adjacentButtons = new Dictionary<ButtonDirection, UiButton>();
            _oppositeDirections = new Dictionary<ButtonDirection, ButtonDirection>();
            _oppositeDirections.Add(ButtonDirection.Right, ButtonDirection.Left);
            _oppositeDirections.Add(ButtonDirection.Left, ButtonDirection.Right);
            _oppositeDirections.Add(ButtonDirection.Up, ButtonDirection.Down);
            _oppositeDirections.Add(ButtonDirection.Down, ButtonDirection.Up);
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
