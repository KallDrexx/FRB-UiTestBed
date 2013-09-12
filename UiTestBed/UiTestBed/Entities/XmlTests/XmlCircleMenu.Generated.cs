
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using UiTestBed.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using UiTestBed.Entities.Games.SlidePuzzle;
using UiTestBed.Entities;
using UiTestBed.Entities.Tutorial;
using UiTestBed.Entities.XmlTests;
using UiTestBed.Entities.XuiLikeDemo;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Graphics.Animation;

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
#endif

#if FRB_XNA && !MONODROID
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace UiTestBed.Entities.XmlTests
{
	public partial class XmlCircleMenu : PositionedObject, IDestroyable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		protected static FlatRedBall.Graphics.Animation.AnimationChainList LoadingAnimation;
		protected static FlatRedBall.Graphics.Animation.AnimationChainList MenuArrow;
		protected static FlatRedBall.Graphics.Animation.AnimationChainList MenuBackground;
		protected static FlatRedBall.Graphics.Animation.AnimationChainList MenuButtonAnimations;
		protected static FlatRedBall.Graphics.Animation.AnimationChainList MenuVolumeBar;
		
		protected Layer LayerProvidedByContainer = null;

        public XmlCircleMenu(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public XmlCircleMenu(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			
			PostInitialize();
			if (addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			// We move this back to the origin and unrotate it so that anything attached to it can just use its absolute position
			float oldRotationX = RotationX;
			float oldRotationY = RotationY;
			float oldRotationZ = RotationZ;
			
			float oldX = X;
			float oldY = Y;
			float oldZ = Z;
			
			X = 0;
			Y = 0;
			Z = 0;
			RotationX = 0;
			RotationY = 0;
			RotationZ = 0;
			X = oldX;
			Y = oldY;
			Z = oldZ;
			RotationX = oldRotationX;
			RotationY = oldRotationY;
			RotationZ = oldRotationZ;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
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
			bool registerUnload = false;
			if (LoadedContentManagers.Contains(contentManagerName) == false)
			{
				LoadedContentManagers.Add(contentManagerName);
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("XmlCircleMenuStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/loadinganimation.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				LoadingAnimation = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/loadinganimation.achx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/menuarrow.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				MenuArrow = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/menuarrow.achx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/menubackground.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				MenuBackground = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/menubackground.achx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/menubuttonanimations.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				MenuButtonAnimations = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/menubuttonanimations.achx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/menuvolumebar.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				MenuVolumeBar = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/xmltests/xmlcirclemenu/menuvolumebar.achx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("XmlCircleMenuStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
				if (LoadingAnimation != null)
				{
					LoadingAnimation= null;
				}
				if (MenuArrow != null)
				{
					MenuArrow= null;
				}
				if (MenuBackground != null)
				{
					MenuBackground= null;
				}
				if (MenuButtonAnimations != null)
				{
					MenuButtonAnimations= null;
				}
				if (MenuVolumeBar != null)
				{
					MenuVolumeBar= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "LoadingAnimation":
					return LoadingAnimation;
				case  "MenuArrow":
					return MenuArrow;
				case  "MenuBackground":
					return MenuBackground;
				case  "MenuButtonAnimations":
					return MenuButtonAnimations;
				case  "MenuVolumeBar":
					return MenuVolumeBar;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "LoadingAnimation":
					return LoadingAnimation;
				case  "MenuArrow":
					return MenuArrow;
				case  "MenuBackground":
					return MenuBackground;
				case  "MenuButtonAnimations":
					return MenuButtonAnimations;
				case  "MenuVolumeBar":
					return MenuVolumeBar;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "LoadingAnimation":
					return LoadingAnimation;
				case  "MenuArrow":
					return MenuArrow;
				case  "MenuBackground":
					return MenuBackground;
				case  "MenuButtonAnimations":
					return MenuButtonAnimations;
				case  "MenuVolumeBar":
					return MenuVolumeBar;
			}
			return null;
		}
		protected bool mIsPaused;
		public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	public static class XmlCircleMenuExtensionMethods
	{
	}
	
}
