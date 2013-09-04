using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Input;
using FlatRedBall.IO;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using UiTestBed.Entities.Games.RpgDemo;
using UiTestBed.Entities.Games.SlidePuzzle;
using UiTestBed.Entities;
using UiTestBed.Entities.Tutorial;
using UiTestBed.Entities.XuiLikeDemo;
using FlatRedBall;
using FlatRedBall.Screens;

namespace UiTestBed.Screens
{
	public partial class MenuDemo : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private UiTestBed.Entities.XuiLikeDemo.LevelSelectMenu LevelSelectMenuInstance;
		private UiTestBed.Entities.XuiLikeDemo.MainMenu MainMenuInstance;
		private UiTestBed.Entities.XuiLikeDemo.OptionsMenu OptionsMenuInstance;
		private UiTestBed.Entities.XuiLikeDemo.LoadingScreen LoadingScreenInstance;

		public MenuDemo()
			: base("MenuDemo")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			LevelSelectMenuInstance = new UiTestBed.Entities.XuiLikeDemo.LevelSelectMenu(ContentManagerName, false);
			LevelSelectMenuInstance.Name = "LevelSelectMenuInstance";
			MainMenuInstance = new UiTestBed.Entities.XuiLikeDemo.MainMenu(ContentManagerName, false);
			MainMenuInstance.Name = "MainMenuInstance";
			OptionsMenuInstance = new UiTestBed.Entities.XuiLikeDemo.OptionsMenu(ContentManagerName, false);
			OptionsMenuInstance.Name = "OptionsMenuInstance";
			LoadingScreenInstance = new UiTestBed.Entities.XuiLikeDemo.LoadingScreen(ContentManagerName, false);
			LoadingScreenInstance.Name = "LoadingScreenInstance";
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				LevelSelectMenuInstance.Activity();
				MainMenuInstance.Activity();
				OptionsMenuInstance.Activity();
				LoadingScreenInstance.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			
			if (LevelSelectMenuInstance != null)
			{
				LevelSelectMenuInstance.Destroy();
				LevelSelectMenuInstance.Detach();
			}
			if (MainMenuInstance != null)
			{
				MainMenuInstance.Destroy();
				MainMenuInstance.Detach();
			}
			if (OptionsMenuInstance != null)
			{
				OptionsMenuInstance.Destroy();
				OptionsMenuInstance.Detach();
			}
			if (LoadingScreenInstance != null)
			{
				LoadingScreenInstance.Destroy();
				LoadingScreenInstance.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			LevelSelectMenuInstance.CurrentState = LevelSelectMenu.VariableState.Inactive;
			MainMenuInstance.CurrentState = MainMenu.VariableState.Deactivated;
			OptionsMenuInstance.CurrentState = OptionsMenu.VariableState.Deactivated;
			LoadingScreenInstance.CurrentState = LoadingScreen.VariableState.Deactivated;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			LevelSelectMenuInstance.AddToManagers(mLayer);
			LevelSelectMenuInstance.CurrentState = LevelSelectMenu.VariableState.Inactive;
			MainMenuInstance.AddToManagers(mLayer);
			MainMenuInstance.CurrentState = MainMenu.VariableState.Deactivated;
			OptionsMenuInstance.AddToManagers(mLayer);
			OptionsMenuInstance.CurrentState = OptionsMenu.VariableState.Deactivated;
			LoadingScreenInstance.AddToManagers(mLayer);
			LoadingScreenInstance.CurrentState = LoadingScreen.VariableState.Deactivated;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			LevelSelectMenuInstance.ConvertToManuallyUpdated();
			MainMenuInstance.ConvertToManuallyUpdated();
			OptionsMenuInstance.ConvertToManuallyUpdated();
			LoadingScreenInstance.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			UiTestBed.Entities.XuiLikeDemo.LevelSelectMenu.LoadStaticContent(contentManagerName);
			UiTestBed.Entities.XuiLikeDemo.MainMenu.LoadStaticContent(contentManagerName);
			UiTestBed.Entities.XuiLikeDemo.OptionsMenu.LoadStaticContent(contentManagerName);
			UiTestBed.Entities.XuiLikeDemo.LoadingScreen.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			return null;
		}
		public static object GetFile (string memberName)
		{
			return null;
		}
		object GetMember (string memberName)
		{
			return null;
		}


	}
}
