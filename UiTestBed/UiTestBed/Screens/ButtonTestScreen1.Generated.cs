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
using UiTestBed.Entities.Layouts;
using UiTestBed.Entities;
using FlatRedBall;
using FlatRedBall.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;

namespace UiTestBed.Screens
{
	public partial class ButtonTestScreen1 : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private FlatRedBall.Graphics.Layer UiLayer;
		private PositionedObjectList<UiButton> Buttons;
		private PositionedObjectList<CircularLayoutManager> CircularLayouts;
		private PositionedObjectList<BoxLayoutManager> BoxLayouts;
		private UiTestBed.Entities.Layouts.SimpleLayoutManager MainLayout;

		public ButtonTestScreen1()
			: base("ButtonTestScreen1")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			UiLayer = new FlatRedBall.Graphics.Layer();
			UiLayer.Name = "UiLayer";
			Buttons = new PositionedObjectList<UiButton>();
			CircularLayouts = new PositionedObjectList<CircularLayoutManager>();
			BoxLayouts = new PositionedObjectList<BoxLayoutManager>();
			MainLayout = new UiTestBed.Entities.Layouts.SimpleLayoutManager(ContentManagerName, false);
			MainLayout.Name = "MainLayout";
			
			
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
			SpriteManager.AddLayer(UiLayer);
			UiLayer.UsePixelCoordinates();
			if (SpriteManager.Camera.Orthogonal)
			{
				UiLayer.LayerCameraSettings.OrthogonalWidth = FlatRedBall.SpriteManager.Camera.OrthogonalWidth;
				UiLayer.LayerCameraSettings.OrthogonalHeight = FlatRedBall.SpriteManager.Camera.OrthogonalHeight;
			}
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				for (int i = Buttons.Count - 1; i > -1; i--)
				{
					if (i < Buttons.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						Buttons[i].Activity();
					}
				}
				for (int i = CircularLayouts.Count - 1; i > -1; i--)
				{
					if (i < CircularLayouts.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						CircularLayouts[i].Activity();
					}
				}
				for (int i = BoxLayouts.Count - 1; i > -1; i--)
				{
					if (i < BoxLayouts.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						BoxLayouts[i].Activity();
					}
				}
				MainLayout.Activity();
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
			
			if (UiLayer != null)
			{
				SpriteManager.RemoveLayer(UiLayer);
			}
			for (int i = Buttons.Count - 1; i > -1; i--)
			{
				Buttons[i].Destroy();
			}
			for (int i = CircularLayouts.Count - 1; i > -1; i--)
			{
				CircularLayouts[i].Destroy();
			}
			for (int i = BoxLayouts.Count - 1; i > -1; i--)
			{
				BoxLayouts[i].Destroy();
			}
			if (MainLayout != null)
			{
				MainLayout.Destroy();
				MainLayout.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			MainLayout.AddToManagers(UiLayer);
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			for (int i = 0; i < Buttons.Count; i++)
			{
				Buttons[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < CircularLayouts.Count; i++)
			{
				CircularLayouts[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < BoxLayouts.Count; i++)
			{
				BoxLayouts[i].ConvertToManuallyUpdated();
			}
			MainLayout.ConvertToManuallyUpdated();
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
			UiTestBed.Entities.Layouts.SimpleLayoutManager.LoadStaticContent(contentManagerName);
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
