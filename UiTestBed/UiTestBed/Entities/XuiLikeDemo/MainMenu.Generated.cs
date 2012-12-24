using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Model;

using FlatRedBall.Input;
using FlatRedBall.Utilities;

using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using UiTestBed.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using UiTestBed.Entities;
using UiTestBed.Entities.XuiLikeDemo;
using FlatRedBall;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework.Graphics;

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

namespace UiTestBed.Entities.XuiLikeDemo
{
	public partial class MainMenu : PositionedObject, IDestroyable
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
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Activated = 2, 
			Deactivated = 3
		}
		protected int mCurrentState = 0;
		public VariableState CurrentState
		{
			get
			{
				if (Enum.IsDefined(typeof(VariableState), mCurrentState))
				{
					return (VariableState)mCurrentState;
				}
				else
				{
					return VariableState.Unknown;
				}
			}
			set
			{
				mCurrentState = (int)value;
				switch(CurrentState)
				{
					case  VariableState.Uninitialized:
						break;
					case  VariableState.Unknown:
						break;
					case  VariableState.Activated:
						OverallAlpha = 1f;
						break;
					case  VariableState.Deactivated:
						OverallAlpha = 0f;
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		private static Microsoft.Xna.Framework.Graphics.Texture2D arrow;
		
		private FlatRedBall.Sprite ArrowSprite;
		public event EventHandler BeforeIsActiveSet;
		public event EventHandler AfterIsActiveSet;
		bool mIsActive = false;
		public bool IsActive
		{
			set
			{
				if (BeforeIsActiveSet != null)
				{
					BeforeIsActiveSet(this, null);
				}
				mIsActive = value;
				if (AfterIsActiveSet != null)
				{
					AfterIsActiveSet(this, null);
				}
			}
			get
			{
				return mIsActive;
			}
		}
		public event EventHandler BeforeOverallAlphaSet;
		public event EventHandler AfterOverallAlphaSet;
		float mOverallAlpha = 1f;
		public float OverallAlpha
		{
			set
			{
				if (BeforeOverallAlphaSet != null)
				{
					BeforeOverallAlphaSet(this, null);
				}
				mOverallAlpha = value;
				if (AfterOverallAlphaSet != null)
				{
					AfterOverallAlphaSet(this, null);
				}
			}
			get
			{
				return mOverallAlpha;
			}
		}
		public float OverallAlphaVelocity = 0;
		public float SecondsToFade = 1f;
		public bool OptionsSelected;
		public int Index { get; set; }
		public bool Used { get; set; }
		protected Layer LayerProvidedByContainer = null;

        public MainMenu(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public MainMenu(string contentManagerName, bool addToManagers) :
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
			ArrowSprite = new FlatRedBall.Sprite();
			this.BeforeIsActiveSet += OnBeforeIsActiveSet;
			this.AfterIsActiveSet += OnAfterIsActiveSet;
			this.AfterOverallAlphaSet += OnAfterOverallAlphaSet;
			
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
			
			if (OverallAlphaVelocity!= 0)
			{
				OverallAlpha += OverallAlphaVelocity * TimeManager.SecondDifference;
			}
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			
			if (ArrowSprite != null)
			{
				ArrowSprite.Detach(); SpriteManager.RemoveSprite(ArrowSprite);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (ArrowSprite.Parent == null)
			{
				ArrowSprite.CopyAbsoluteToRelative();
				ArrowSprite.AttachTo(this, false);
			}
			ArrowSprite.PixelSize = 0.5f;
			ArrowSprite.Texture = arrow;
			ArrowSprite.Visible = false;
			IsActive = false;
			OverallAlpha = 1f;
			SecondsToFade = 1f;
			CurrentState = MainMenu.VariableState.Deactivated;
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
			SpriteManager.AddToLayer(ArrowSprite, layerToAddTo);
			ArrowSprite.PixelSize = 0.5f;
			ArrowSprite.Texture = arrow;
			ArrowSprite.Visible = false;
			IsActive = false;
			OverallAlpha = 1f;
			SecondsToFade = 1f;
			CurrentState = MainMenu.VariableState.Deactivated;
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
			SpriteManager.ConvertToManuallyUpdated(ArrowSprite);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("MainMenuStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/xuilikedemo/mainmenu/arrow.png", ContentManagerName))
				{
					registerUnload = true;
				}
				arrow = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/xuilikedemo/mainmenu/arrow.png", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("MainMenuStaticUnload", UnloadStaticContent);
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
				if (arrow != null)
				{
					arrow= null;
				}
			}
		}
		static VariableState mLoadingState = VariableState.Uninitialized;
		public static VariableState LoadingState
		{
			get
			{
				return mLoadingState;
			}
			set
			{
				mLoadingState = value;
			}
		}
		public Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  VariableState.Activated:
					OverallAlphaVelocity = (1f - OverallAlpha) / (float)secondsToTake;
					break;
				case  VariableState.Deactivated:
					OverallAlphaVelocity = (0f - OverallAlpha) / (float)secondsToTake;
					break;
			}
			var instruction = new DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = TimeManager.CurrentTime + secondsToTake;
			this.Instructions.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (VariableState stateToStop)
		{
			switch(stateToStop)
			{
				case  VariableState.Activated:
					OverallAlphaVelocity =  0;
					break;
				case  VariableState.Deactivated:
					OverallAlphaVelocity =  0;
					break;
			}
			CurrentState = stateToStop;
		}
		public void InterpolateBetween (VariableState firstState, VariableState secondState, float interpolationValue)
		{
			#if DEBUG
			if (float.IsNaN(interpolationValue))
			{
				throw new Exception("interpolationValue cannot be NaN");
			}
			#endif
			bool setOverallAlpha = true;
			float OverallAlphaFirstValue= 0;
			float OverallAlphaSecondValue= 0;
			switch(firstState)
			{
				case  VariableState.Activated:
					OverallAlphaFirstValue = 1f;
					break;
				case  VariableState.Deactivated:
					OverallAlphaFirstValue = 0f;
					break;
			}
			switch(secondState)
			{
				case  VariableState.Activated:
					OverallAlphaSecondValue = 1f;
					break;
				case  VariableState.Deactivated:
					OverallAlphaSecondValue = 0f;
					break;
			}
			if (setOverallAlpha)
			{
				OverallAlpha = OverallAlphaFirstValue * (1 - interpolationValue) + OverallAlphaSecondValue * interpolationValue;
			}
		}
		public static void PreloadStateContent (VariableState state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			object throwaway;
			switch(state)
			{
				case  VariableState.Activated:
					break;
				case  VariableState.Deactivated:
					break;
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "arrow":
					return arrow;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "arrow":
					return arrow;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "arrow":
					return arrow;
			}
			return null;
		}
		protected bool mIsPaused;
		public override void Pause (InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			InstructionManager.IgnorePausingFor(this);
			InstructionManager.IgnorePausingFor(ArrowSprite);
		}

    }
	
	
	// Extra classes
	public static class MainMenuExtensionMethods
	{
	}
	
}
