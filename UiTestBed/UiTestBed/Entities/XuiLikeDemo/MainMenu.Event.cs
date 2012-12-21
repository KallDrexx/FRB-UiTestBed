using System;
using FlatRedBall;
using FlatRedBall.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using FlatRedBall.Instructions;
using UiTestBed.Entities;
using UiTestBed.Entities.XuiLikeDemo;
using UiTestBed.Screens;

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class MainMenu
	{
        protected bool _previousActiveState;

		void OnAfterIsActiveSet (object sender, EventArgs e)
        {
            // Only do something if the previous active status actually changed
            if (_previousActiveState != IsActive)
            {
                if (IsActive)
                    _mainGroup.FocusNextControl();
                else
                    _mainGroup.UnfocusCurrentControl();
            }
        }        
        
        void OnBeforeIsActiveSet (object sender, EventArgs e)
        {
            _previousActiveState = IsActive;
        }

        void OnAfterAlphaSet (object sender, EventArgs e)
        {
            if (_layout != null)
                _layout.Alpha = Alpha;
        }
	}
}
