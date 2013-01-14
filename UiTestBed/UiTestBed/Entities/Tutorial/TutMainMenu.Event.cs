using System;
using FlatRedBall;
using FlatRedBall.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using FlatRedBall.Instructions;
using UiTestBed.Entities;
using UiTestBed.Entities.Tutorial;
using UiTestBed.Entities.XuiLikeDemo;
using UiTestBed.Screens;
namespace UiTestBed.Entities.Tutorial
{
	public partial class TutMainMenu
	{
        void OnAfterOverallAlphaSet (object sender, EventArgs e)
        {
            	if (_layout != null)
                	_layout.Alpha = OverallAlpha;
        }

	}
}
