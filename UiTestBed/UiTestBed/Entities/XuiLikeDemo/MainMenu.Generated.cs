
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
		public enum ChosenMenuOption
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Quit = 2, 
			Options = 3, 
			LevelSelect = 4, 
			None = 5
		}
		protected int mCurrentChosenMenuOptionState = 0;
		public ChosenMenuOption CurrentChosenMenuOptionState
		{
			get
			{
				if (Enum.IsDefined(typeof(ChosenMenuOption), mCurrentChosenMenuOptionState))
				{
					return (ChosenMenuOption)mCurrentChosenMenuOptionState;
				}
				else
				{
					return ChosenMenuOption.Unknown;
				}
			}
			set
			{
				mCurrentChosenMenuOptionState = (int)value;
				switch(CurrentChosenMenuOptionState)
				{
					case  ChosenMenuOption.Uninitialized:
						break;
					case  ChosenMenuOption.Unknown:
						break;
					case  ChosenMenuOption.Quit:
						break;
					case  ChosenMenuOption.Options:
						break;
					case  ChosenMenuOption.LevelSelect:
						break;
					case  ChosenMenuOption.None:
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		protected static Microsoft.Xna.Framework.Graphics.Texture2D arrow;
		
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
			this.BeforeIsActiveSet += OnBeforeIsActiveSet;
			this.AfterIsActiveSet += OnAfterIsActiveSet;
			this.AfterOverallAlphaSet += OnAfterOverallAlphaSet;
			ArrowSprite.Texture = arrow;
			ArrowSprite.PixelSize = 0.5f;
			ArrowSprite.Visible = false;
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
			ArrowSprite.Texture = arrow;
			ArrowSprite.PixelSize = 0.5f;
			ArrowSprite.Visible = false;
			X = oldX;
			Y = oldY;
			Z = oldZ;
			RotationX = oldRotationX;
			RotationY = oldRotationY;
			RotationZ = oldRotationZ;
			IsActive = false;
			OverallAlpha = 1f;
			SecondsToFade = 1f;
			CurrentState = MainMenu.VariableState.Deactivated;
			CurrentChosenMenuOptionState = MainMenu.ChosenMenuOption.None;
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
		public FlatRedBall.Instructions.Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
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
			var instruction = new FlatRedBall.Instructions.DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + secondsToTake;
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
			if (interpolationValue < 1)
			{
				mCurrentState = (int)firstState;
			}
			else
			{
				mCurrentState = (int)secondState;
			}
		}
		public FlatRedBall.Instructions.Instruction InterpolateToState (ChosenMenuOption stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  ChosenMenuOption.Quit:
					break;
				case  ChosenMenuOption.Options:
					break;
				case  ChosenMenuOption.LevelSelect:
					break;
				case  ChosenMenuOption.None:
					break;
			}
			var instruction = new FlatRedBall.Instructions.DelegateInstruction<ChosenMenuOption>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + secondsToTake;
			this.Instructions.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (ChosenMenuOption stateToStop)
		{
			switch(stateToStop)
			{
				case  ChosenMenuOption.Quit:
					break;
				case  ChosenMenuOption.Options:
					break;
				case  ChosenMenuOption.LevelSelect:
					break;
				case  ChosenMenuOption.None:
					break;
			}
			CurrentChosenMenuOptionState = stateToStop;
		}
		public void InterpolateBetween (ChosenMenuOption firstState, ChosenMenuOption secondState, float interpolationValue)
		{
			#if DEBUG
			if (float.IsNaN(interpolationValue))
			{
				throw new Exception("interpolationValue cannot be NaN");
			}
			#endif
			switch(firstState)
			{
				case  ChosenMenuOption.Quit:
					break;
				case  ChosenMenuOption.Options:
					break;
				case  ChosenMenuOption.LevelSelect:
					break;
				case  ChosenMenuOption.None:
					break;
			}
			switch(secondState)
			{
				case  ChosenMenuOption.Quit:
					break;
				case  ChosenMenuOption.Options:
					break;
				case  ChosenMenuOption.LevelSelect:
					break;
				case  ChosenMenuOption.None:
					break;
			}
			if (interpolationValue < 1)
			{
				mCurrentChosenMenuOptionState = (int)firstState;
			}
			else
			{
				mCurrentChosenMenuOptionState = (int)secondState;
			}
		}
		public static void PreloadStateContent (VariableState state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			switch(state)
			{
				case  VariableState.Activated:
					break;
				case  VariableState.Deactivated:
					break;
			}
		}
		public static void PreloadStateContent (ChosenMenuOption state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			switch(state)
			{
				case  ChosenMenuOption.Quit:
					break;
				case  ChosenMenuOption.Options:
					break;
				case  ChosenMenuOption.LevelSelect:
					break;
				case  ChosenMenuOption.None:
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
		public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(ArrowSprite);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(ArrowSprite);
			}
			SpriteManager.AddToLayer(ArrowSprite, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	public static class MainMenuExtensionMethods
	{
	}
	
}
