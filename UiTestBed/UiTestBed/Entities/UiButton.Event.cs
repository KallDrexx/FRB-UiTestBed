using System;
using FlatRedBall;
using FlatRedBall.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using UiTestBed.Entities;
using UiTestBed.Screens;

namespace UiTestBed.Entities
{
	public partial class UiButton
	{
        void OnRollOn (FlatRedBall.Gui.IWindow callingWindow)
        {
            Select();
        }

        void OnRollOff (FlatRedBall.Gui.IWindow callingWindow)
        {
            UnSelect();
        }        
        
        void OnStartPushed()
        {
            PressButton();
        }
        
        void OnClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            PressButton();
            ReleaseButton();
        }

	}
}
