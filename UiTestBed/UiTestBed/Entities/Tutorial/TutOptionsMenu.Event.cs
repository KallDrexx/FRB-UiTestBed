using System;
using FlatRedBall;
using FlatRedBall.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using FlatRedBall.Instructions;
using UiTestBed.Entities.Games.SlidePuzzle;
using UiTestBed.Entities;
using UiTestBed.Entities.Tutorial;
using UiTestBed.Entities.XuiLikeDemo;
using UiTestBed.Screens;
namespace UiTestBed.Entities.Tutorial
{
	public partial class TutOptionsMenu
	{
        void OnAfterVolumeLevelSet (object sender, EventArgs e)
        {
            // Refresh the bar visibility
            if (_volumeBars != null)
                for (int x = 0; x < _volumeBars.Count; x++)
                    _volumeBars[x].Visible = (x < VolumeLevel);
        }

        void OnAfterCurrentDifficultyStateSet (object sender, EventArgs e)
        {
            if (_difficultyText != null)
                _difficultyText.DisplayText = CurrentDifficultyState.ToString();
        }
	}
}
