
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
	public partial class OptionsMenu : PositionedObject, IDestroyable
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
		public enum Difficulty
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Easy = 2, 
			Normal = 3, 
			Hard = 4
		}
		protected int mCurrentDifficultyState = 0;
		public Difficulty CurrentDifficultyState
		{
			get
			{
				if (Enum.IsDefined(typeof(Difficulty), mCurrentDifficultyState))
				{
					return (Difficulty)mCurrentDifficultyState;
				}
				else
				{
					return Difficulty.Unknown;
				}
			}
			set
			{
				mCurrentDifficultyState = (int)value;
				switch(CurrentDifficultyState)
				{
					case  Difficulty.Uninitialized:
						break;
					case  Difficulty.Unknown:
						break;
					case  Difficulty.Easy:
						break;
					case  Difficulty.Normal:
						break;
					case  Difficulty.Hard:
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		
		public event EventHandler BeforeOverallAlphaSet;
		public event EventHandler AfterOverallAlphaSet;
		float mOverallAlpha;
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
		public int VolumeLevel = 3;
		public bool IsActive = false;
		public float SecondsToFade = 1f;
		public bool MenuExited = false;
		public int MaximumVolumeLevel = 10;
		protected Layer LayerProvidedByContainer = null;

        public OptionsMenu(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public OptionsMenu(string contentManagerName, bool addToManagers) :
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
			


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			this.BeforeOverallAlphaSet += OnBeforeOverallAlphaSet;
			this.AfterOverallAlphaSet += OnAfterOverallAlphaSet;
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
			CurrentState = OptionsMenu.VariableState.Deactivated;
			CurrentDifficultyState = OptionsMenu.Difficulty.Easy;
			VolumeLevel = 3;
			IsActive = false;
			SecondsToFade = 1f;
			MenuExited = false;
			MaximumVolumeLevel = 10;
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("OptionsMenuStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("OptionsMenuStaticUnload", UnloadStaticContent);
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
		public FlatRedBall.Instructions.Instruction InterpolateToState (Difficulty stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  Difficulty.Easy:
					break;
				case  Difficulty.Normal:
					break;
				case  Difficulty.Hard:
					break;
			}
			var instruction = new FlatRedBall.Instructions.DelegateInstruction<Difficulty>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + secondsToTake;
			this.Instructions.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (Difficulty stateToStop)
		{
			switch(stateToStop)
			{
				case  Difficulty.Easy:
					break;
				case  Difficulty.Normal:
					break;
				case  Difficulty.Hard:
					break;
			}
			CurrentDifficultyState = stateToStop;
		}
		public void InterpolateBetween (Difficulty firstState, Difficulty secondState, float interpolationValue)
		{
			#if DEBUG
			if (float.IsNaN(interpolationValue))
			{
				throw new Exception("interpolationValue cannot be NaN");
			}
			#endif
			switch(firstState)
			{
				case  Difficulty.Easy:
					break;
				case  Difficulty.Normal:
					break;
				case  Difficulty.Hard:
					break;
			}
			switch(secondState)
			{
				case  Difficulty.Easy:
					break;
				case  Difficulty.Normal:
					break;
				case  Difficulty.Hard:
					break;
			}
			if (interpolationValue < 1)
			{
				mCurrentDifficultyState = (int)firstState;
			}
			else
			{
				mCurrentDifficultyState = (int)secondState;
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
		public static void PreloadStateContent (Difficulty state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			switch(state)
			{
				case  Difficulty.Easy:
					break;
				case  Difficulty.Normal:
					break;
				case  Difficulty.Hard:
					break;
			}
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
	public static class OptionsMenuExtensionMethods
	{
	}
	
}
