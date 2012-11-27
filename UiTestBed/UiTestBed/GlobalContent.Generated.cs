using System.Collections.Generic;
using System.Threading;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using FlatRedBall.Localization;

namespace UiTestBed
{
	public static class GlobalContent
	{
		
		public static FlatRedBall.Graphics.Animation.AnimationChainList Button1 { get; set; }
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "Button1":
					return Button1;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "Button1":
					return Button1;
			}
			return null;
		}
		public static bool IsInitialized { get; private set; }
		public static bool ShouldStopLoading { get; set; }
		static string ContentManagerName = "Global";
		public static void Initialize ()
		{
			
			Button1 = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/globalcontent/button1.achx", ContentManagerName);
						IsInitialized = true;
		}
		
		
	}
}
