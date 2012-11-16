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
using FlatRedBall.Math;
using FlatRedBall.Graphics;

namespace UiTestBed.Screens
{
	public partial class ButtonTestScreen1 : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private PositionedObjectList<UiButton> UiButtonList;
		private FlatRedBall.Graphics.Layer UiLayer;
		private UiTestBed.Entities.Layouts.BoxLayoutManager BoxLayoutRight;
		private UiTestBed.Entities.Layouts.BoxLayoutManager BoxLayoutLeft;
		private UiTestBed.Entities.Layouts.BoxLayoutManager BoxLayoutDown;
		private UiTestBed.Entities.Layouts.BoxLayoutManager BoxLayoutUp;

		public ButtonTestScreen1()
			: base("ButtonTestScreen1")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			UiButtonList = new PositionedObjectList<UiButton>();
			UiLayer = new FlatRedBall.Graphics.Layer();
			UiLayer.Name = "UiLayer";
			BoxLayoutRight = new UiTestBed.Entities.Layouts.BoxLayoutManager(ContentManagerName, false);
			BoxLayoutRight.Name = "BoxLayoutRight";
			BoxLayoutLeft = new UiTestBed.Entities.Layouts.BoxLayoutManager(ContentManagerName, false);
			BoxLayoutLeft.Name = "BoxLayoutLeft";
			BoxLayoutDown = new UiTestBed.Entities.Layouts.BoxLayoutManager(ContentManagerName, false);
			BoxLayoutDown.Name = "BoxLayoutDown";
			BoxLayoutUp = new UiTestBed.Entities.Layouts.BoxLayoutManager(ContentManagerName, false);
			BoxLayoutUp.Name = "BoxLayoutUp";
			
			
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
				
				for (int i = UiButtonList.Count - 1; i > -1; i--)
				{
					if (i < UiButtonList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						UiButtonList[i].Activity();
					}
				}
				BoxLayoutRight.Activity();
				BoxLayoutLeft.Activity();
				BoxLayoutDown.Activity();
				BoxLayoutUp.Activity();
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
			
			for (int i = UiButtonList.Count - 1; i > -1; i--)
			{
				UiButtonList[i].Destroy();
			}
			if (UiLayer != null)
			{
				SpriteManager.RemoveLayer(UiLayer);
			}
			if (BoxLayoutRight != null)
			{
				BoxLayoutRight.Destroy();
				BoxLayoutRight.Detach();
			}
			if (BoxLayoutLeft != null)
			{
				BoxLayoutLeft.Destroy();
				BoxLayoutLeft.Detach();
			}
			if (BoxLayoutDown != null)
			{
				BoxLayoutDown.Destroy();
				BoxLayoutDown.Detach();
			}
			if (BoxLayoutUp != null)
			{
				BoxLayoutUp.Destroy();
				BoxLayoutUp.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (BoxLayoutRight!= null && BoxLayoutRight.Parent == null)
			{
				BoxLayoutRight.CopyAbsoluteToRelative();
				BoxLayoutRight.RelativeZ += -40;
				BoxLayoutRight.AttachTo(SpriteManager.Camera, false);
			}
			BoxLayoutRight.Spacing = 5f;
			BoxLayoutRight.Margin = 10f;
			BoxLayoutRight.CurrentDirectionState = BoxLayoutManager.Direction.Right;
			if (BoxLayoutRight.Parent == null)
			{
				BoxLayoutRight.X = -350f;
			}
			else
			{
				BoxLayoutRight.RelativeX = -350f;
			}
			if (BoxLayoutRight.Parent == null)
			{
				BoxLayoutRight.Y = 250f;
			}
			else
			{
				BoxLayoutRight.RelativeY = 250f;
			}
			if (BoxLayoutLeft!= null && BoxLayoutLeft.Parent == null)
			{
				BoxLayoutLeft.CopyAbsoluteToRelative();
				BoxLayoutLeft.RelativeZ += -40;
				BoxLayoutLeft.AttachTo(SpriteManager.Camera, false);
			}
			BoxLayoutLeft.Spacing = 5f;
			BoxLayoutLeft.Margin = 10f;
			BoxLayoutLeft.CurrentDirectionState = BoxLayoutManager.Direction.Left;
			if (BoxLayoutLeft.Parent == null)
			{
				BoxLayoutLeft.X = 350f;
			}
			else
			{
				BoxLayoutLeft.RelativeX = 350f;
			}
			if (BoxLayoutLeft.Parent == null)
			{
				BoxLayoutLeft.Y = -250f;
			}
			else
			{
				BoxLayoutLeft.RelativeY = -250f;
			}
			if (BoxLayoutDown!= null && BoxLayoutDown.Parent == null)
			{
				BoxLayoutDown.CopyAbsoluteToRelative();
				BoxLayoutDown.RelativeZ += -40;
				BoxLayoutDown.AttachTo(SpriteManager.Camera, false);
			}
			BoxLayoutDown.Spacing = 5f;
			BoxLayoutDown.Margin = 0f;
			BoxLayoutDown.CurrentDirectionState = BoxLayoutManager.Direction.Down;
			if (BoxLayoutDown.Parent == null)
			{
				BoxLayoutDown.X = 350f;
			}
			else
			{
				BoxLayoutDown.RelativeX = 350f;
			}
			if (BoxLayoutDown.Parent == null)
			{
				BoxLayoutDown.Y = 250f;
			}
			else
			{
				BoxLayoutDown.RelativeY = 250f;
			}
			if (BoxLayoutUp!= null && BoxLayoutUp.Parent == null)
			{
				BoxLayoutUp.CopyAbsoluteToRelative();
				BoxLayoutUp.RelativeZ += -40;
				BoxLayoutUp.AttachTo(SpriteManager.Camera, false);
			}
			BoxLayoutUp.Spacing = 5f;
			BoxLayoutUp.Margin = 0f;
			BoxLayoutUp.CurrentDirectionState = BoxLayoutManager.Direction.Up;
			if (BoxLayoutUp.Parent == null)
			{
				BoxLayoutUp.X = -350f;
			}
			else
			{
				BoxLayoutUp.RelativeX = -350f;
			}
			if (BoxLayoutUp.Parent == null)
			{
				BoxLayoutUp.Y = -250f;
			}
			else
			{
				BoxLayoutUp.RelativeY = -250f;
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			// We move the main Camera back to the origin and unrotate it so that anything attached to it can just use its absolute position
			float oldCameraRotationX = SpriteManager.Camera.RotationX;
			float oldCameraRotationY = SpriteManager.Camera.RotationY;
			float oldCameraRotationZ = SpriteManager.Camera.RotationZ;
			
			float oldCameraX = SpriteManager.Camera.X;
			float oldCameraY = SpriteManager.Camera.Y;
			float oldCameraZ = SpriteManager.Camera.Z;
			
			SpriteManager.Camera.X = 0;
			SpriteManager.Camera.Y = 0;
			SpriteManager.Camera.Z = 40; // Move it to 40 so that things attach in front of the camera.
			SpriteManager.Camera.RotationX = 0;
			SpriteManager.Camera.RotationY = 0;
			SpriteManager.Camera.RotationZ = 0;
			BoxLayoutRight.AddToManagers(UiLayer);
			BoxLayoutRight.Spacing = 5f;
			BoxLayoutRight.Margin = 10f;
			BoxLayoutRight.CurrentDirectionState = BoxLayoutManager.Direction.Right;
			if (BoxLayoutRight.Parent == null)
			{
				BoxLayoutRight.X = -350f;
			}
			else
			{
				BoxLayoutRight.RelativeX = -350f;
			}
			if (BoxLayoutRight.Parent == null)
			{
				BoxLayoutRight.Y = 250f;
			}
			else
			{
				BoxLayoutRight.RelativeY = 250f;
			}
			BoxLayoutLeft.AddToManagers(UiLayer);
			BoxLayoutLeft.Spacing = 5f;
			BoxLayoutLeft.Margin = 10f;
			BoxLayoutLeft.CurrentDirectionState = BoxLayoutManager.Direction.Left;
			if (BoxLayoutLeft.Parent == null)
			{
				BoxLayoutLeft.X = 350f;
			}
			else
			{
				BoxLayoutLeft.RelativeX = 350f;
			}
			if (BoxLayoutLeft.Parent == null)
			{
				BoxLayoutLeft.Y = -250f;
			}
			else
			{
				BoxLayoutLeft.RelativeY = -250f;
			}
			BoxLayoutDown.AddToManagers(UiLayer);
			BoxLayoutDown.Spacing = 5f;
			BoxLayoutDown.Margin = 0f;
			BoxLayoutDown.CurrentDirectionState = BoxLayoutManager.Direction.Down;
			if (BoxLayoutDown.Parent == null)
			{
				BoxLayoutDown.X = 350f;
			}
			else
			{
				BoxLayoutDown.RelativeX = 350f;
			}
			if (BoxLayoutDown.Parent == null)
			{
				BoxLayoutDown.Y = 250f;
			}
			else
			{
				BoxLayoutDown.RelativeY = 250f;
			}
			BoxLayoutUp.AddToManagers(UiLayer);
			BoxLayoutUp.Spacing = 5f;
			BoxLayoutUp.Margin = 0f;
			BoxLayoutUp.CurrentDirectionState = BoxLayoutManager.Direction.Up;
			if (BoxLayoutUp.Parent == null)
			{
				BoxLayoutUp.X = -350f;
			}
			else
			{
				BoxLayoutUp.RelativeX = -350f;
			}
			if (BoxLayoutUp.Parent == null)
			{
				BoxLayoutUp.Y = -250f;
			}
			else
			{
				BoxLayoutUp.RelativeY = -250f;
			}
			SpriteManager.Camera.X = oldCameraX;
			SpriteManager.Camera.Y = oldCameraY;
			SpriteManager.Camera.Z = oldCameraZ;
			SpriteManager.Camera.RotationX = oldCameraRotationX;
			SpriteManager.Camera.RotationY = oldCameraRotationY;
			SpriteManager.Camera.RotationZ = oldCameraRotationZ;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			for (int i = 0; i < UiButtonList.Count; i++)
			{
				UiButtonList[i].ConvertToManuallyUpdated();
			}
			BoxLayoutRight.ConvertToManuallyUpdated();
			BoxLayoutLeft.ConvertToManuallyUpdated();
			BoxLayoutDown.ConvertToManuallyUpdated();
			BoxLayoutUp.ConvertToManuallyUpdated();
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
			UiTestBed.Entities.Layouts.BoxLayoutManager.LoadStaticContent(contentManagerName);
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
