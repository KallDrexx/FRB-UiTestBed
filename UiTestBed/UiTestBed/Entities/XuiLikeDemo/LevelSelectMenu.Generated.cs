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
using UiTestBed.Entities.Games.RpgDemo;
using UiTestBed.Entities.Games.SlidePuzzle;
using UiTestBed.Entities;
using UiTestBed.Entities.Tutorial;
using UiTestBed.Entities.XuiLikeDemo;
using FlatRedBall;
using FlatRedBall.Screens;

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
	public partial class LevelSelectMenu : PositionedObject, IDestroyable
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
			WorldSelected = 2, 
			WorldUnSelected = 3, 
			Inactive = 4
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
					case  VariableState.WorldSelected:
						SelectedGridSpacing = 20f;
						IsActive = true;
						OverallAlpha = 1f;
						break;
					case  VariableState.WorldUnSelected:
						SelectedGridSpacing = 0f;
						IsActive = true;
						OverallAlpha = 1f;
						break;
					case  VariableState.Inactive:
						SelectedGridSpacing = 0f;
						IsActive = false;
						OverallAlpha = 0f;
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		
		public event EventHandler BeforeSelectedGridSpacingSet;
		public event EventHandler AfterSelectedGridSpacingSet;
		float mSelectedGridSpacing = 0f;
		public float SelectedGridSpacing
		{
			set
			{
				if (BeforeSelectedGridSpacingSet != null)
				{
					BeforeSelectedGridSpacingSet(this, null);
				}
				mSelectedGridSpacing = value;
				if (AfterSelectedGridSpacingSet != null)
				{
					AfterSelectedGridSpacingSet(this, null);
				}
			}
			get
			{
				return mSelectedGridSpacing;
			}
		}
		public float SelectedGridSpacingVelocity = 0;
		public bool IsActive;
		public event EventHandler BeforeOverallAlphaSet;
		public event EventHandler AfterOverallAlphaSet;
		float mOverallAlpha = 0f;
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
		public bool MenuExited;
		public string LevelToLoad;
		public int Index { get; set; }
		public bool Used { get; set; }
		protected Layer LayerProvidedByContainer = null;

        public LevelSelectMenu(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public LevelSelectMenu(string contentManagerName, bool addToManagers) :
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
			
			if (SelectedGridSpacingVelocity!= 0)
			{
				SelectedGridSpacing += SelectedGridSpacingVelocity * TimeManager.SecondDifference;
			}
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
			this.AfterSelectedGridSpacingSet += OnAfterSelectedGridSpacingSet;
			this.AfterOverallAlphaSet += OnAfterOverallAlphaSet;
			CurrentState = LevelSelectMenu.VariableState.Inactive;
			SelectedGridSpacing = 0f;
			OverallAlpha = 0f;
			SecondsToFade = 1f;
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
			CurrentState = LevelSelectMenu.VariableState.Inactive;
			SelectedGridSpacing = 0f;
			OverallAlpha = 0f;
			SecondsToFade = 1f;
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("LevelSelectMenuStaticUnload", UnloadStaticContent);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("LevelSelectMenuStaticUnload", UnloadStaticContent);
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
		public Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  VariableState.WorldSelected:
					SelectedGridSpacingVelocity = (20f - SelectedGridSpacing) / (float)secondsToTake;
					OverallAlphaVelocity = (1f - OverallAlpha) / (float)secondsToTake;
					break;
				case  VariableState.WorldUnSelected:
					SelectedGridSpacingVelocity = (0f - SelectedGridSpacing) / (float)secondsToTake;
					OverallAlphaVelocity = (1f - OverallAlpha) / (float)secondsToTake;
					break;
				case  VariableState.Inactive:
					SelectedGridSpacingVelocity = (0f - SelectedGridSpacing) / (float)secondsToTake;
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
				case  VariableState.WorldSelected:
					SelectedGridSpacingVelocity =  0;
					OverallAlphaVelocity =  0;
					break;
				case  VariableState.WorldUnSelected:
					SelectedGridSpacingVelocity =  0;
					OverallAlphaVelocity =  0;
					break;
				case  VariableState.Inactive:
					SelectedGridSpacingVelocity =  0;
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
			bool setSelectedGridSpacing = true;
			float SelectedGridSpacingFirstValue= 0;
			float SelectedGridSpacingSecondValue= 0;
			bool setOverallAlpha = true;
			float OverallAlphaFirstValue= 0;
			float OverallAlphaSecondValue= 0;
			switch(firstState)
			{
				case  VariableState.WorldSelected:
					SelectedGridSpacingFirstValue = 20f;
					if (interpolationValue < 1)
					{
						this.IsActive = true;
					}
					OverallAlphaFirstValue = 1f;
					break;
				case  VariableState.WorldUnSelected:
					SelectedGridSpacingFirstValue = 0f;
					if (interpolationValue < 1)
					{
						this.IsActive = true;
					}
					OverallAlphaFirstValue = 1f;
					break;
				case  VariableState.Inactive:
					SelectedGridSpacingFirstValue = 0f;
					if (interpolationValue < 1)
					{
						this.IsActive = false;
					}
					OverallAlphaFirstValue = 0f;
					break;
			}
			switch(secondState)
			{
				case  VariableState.WorldSelected:
					SelectedGridSpacingSecondValue = 20f;
					if (interpolationValue >= 1)
					{
						this.IsActive = true;
					}
					OverallAlphaSecondValue = 1f;
					break;
				case  VariableState.WorldUnSelected:
					SelectedGridSpacingSecondValue = 0f;
					if (interpolationValue >= 1)
					{
						this.IsActive = true;
					}
					OverallAlphaSecondValue = 1f;
					break;
				case  VariableState.Inactive:
					SelectedGridSpacingSecondValue = 0f;
					if (interpolationValue >= 1)
					{
						this.IsActive = false;
					}
					OverallAlphaSecondValue = 0f;
					break;
			}
			if (setSelectedGridSpacing)
			{
				SelectedGridSpacing = SelectedGridSpacingFirstValue * (1 - interpolationValue) + SelectedGridSpacingSecondValue * interpolationValue;
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
		public static void PreloadStateContent (VariableState state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			object throwaway;
			switch(state)
			{
				case  VariableState.WorldSelected:
					break;
				case  VariableState.WorldUnSelected:
					break;
				case  VariableState.Inactive:
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
		public override void Pause (InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			InstructionManager.IgnorePausingFor(this);
		}
		public void MoveToLayer (Layer layerToMoveTo)
		{
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	public static class LevelSelectMenuExtensionMethods
	{
	}
	
}
