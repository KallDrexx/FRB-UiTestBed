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
using UiTestBed.Entities.Layouts;
using UiTestBed.Entities;
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

namespace UiTestBed.Entities.Layouts
{
	public partial class CircularLayoutManager : PositionedObject, IDestroyable, IVisible
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
		public enum ArrangementMode
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Clockwise = 2, 
			EvenlySpaced = 3, 
			Manual = 4, 
			CounterClockwise = 5
		}
		protected int mCurrentArrangementModeState = 0;
		public ArrangementMode CurrentArrangementModeState
		{
			get
			{
				if (Enum.IsDefined(typeof(ArrangementMode), mCurrentArrangementModeState))
				{
					return (ArrangementMode)mCurrentArrangementModeState;
				}
				else
				{
					return ArrangementMode.Unknown;
				}
			}
			set
			{
				mCurrentArrangementModeState = (int)value;
				switch(CurrentArrangementModeState)
				{
					case  ArrangementMode.Uninitialized:
						break;
					case  ArrangementMode.Unknown:
						break;
					case  ArrangementMode.Clockwise:
						break;
					case  ArrangementMode.EvenlySpaced:
						break;
					case  ArrangementMode.Manual:
						break;
					case  ArrangementMode.CounterClockwise:
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		
		public float Radius = 100f;
		public float Margin = 0f;
		public event EventHandler BeforeStartingDegreesSet;
		public event EventHandler AfterStartingDegreesSet;
		float mStartingDegrees = 0f;
		public float StartingDegrees
		{
			set
			{
				if (BeforeStartingDegreesSet != null)
				{
					BeforeStartingDegreesSet(this, null);
				}
				mStartingDegrees = value;
				if (AfterStartingDegreesSet != null)
				{
					AfterStartingDegreesSet(this, null);
				}
			}
			get
			{
				return mStartingDegrees;
			}
		}
		public float MinDegreeOffset;
		public int Index { get; set; }
		public bool Used { get; set; }
		public event EventHandler BeforeVisibleSet;
		public event EventHandler AfterVisibleSet;
		protected bool mVisible = true;
		public virtual bool Visible
		{
			get
			{
				return mVisible;
			}
			set
			{
				if (BeforeVisibleSet != null)
				{
					BeforeVisibleSet(this, null);
				}
				mVisible = value;
				if (AfterVisibleSet != null)
				{
					AfterVisibleSet(this, null);
				}
			}
		}
		public bool IgnoresParentVisibility { get; set; }
		public bool AbsoluteVisible
		{
			get
			{
				return Visible && (Parent == null || IgnoresParentVisibility || Parent is IVisible == false || (Parent as IVisible).AbsoluteVisible);
			}
		}
		IVisible IVisible.Parent
		{
			get
			{
				if (this.Parent != null && this.Parent is IVisible)
				{
					return this.Parent as IVisible;
				}
				else
				{
					return null;
				}
			}
		}
		protected Layer LayerProvidedByContainer = null;

        public CircularLayoutManager(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public CircularLayoutManager(string contentManagerName, bool addToManagers) :
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
			this.AfterStartingDegreesSet += OnAfterStartingDegreesSet;
			
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
			CurrentArrangementModeState = CircularLayoutManager.ArrangementMode.Manual;
			Radius = 100f;
			Margin = 0f;
			StartingDegrees = 0f;
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
			CurrentArrangementModeState = CircularLayoutManager.ArrangementMode.Manual;
			Radius = 100f;
			Margin = 0f;
			StartingDegrees = 0f;
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("CircularLayoutManagerStaticUnload", UnloadStaticContent);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("CircularLayoutManagerStaticUnload", UnloadStaticContent);
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
		public Instruction InterpolateToState (ArrangementMode stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  ArrangementMode.Clockwise:
					break;
				case  ArrangementMode.EvenlySpaced:
					break;
				case  ArrangementMode.Manual:
					break;
				case  ArrangementMode.CounterClockwise:
					break;
			}
			var instruction = new DelegateInstruction<ArrangementMode>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = TimeManager.CurrentTime + secondsToTake;
			this.Instructions.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (ArrangementMode stateToStop)
		{
			switch(stateToStop)
			{
				case  ArrangementMode.Clockwise:
					break;
				case  ArrangementMode.EvenlySpaced:
					break;
				case  ArrangementMode.Manual:
					break;
				case  ArrangementMode.CounterClockwise:
					break;
			}
			CurrentArrangementModeState = stateToStop;
		}
		public void InterpolateBetween (ArrangementMode firstState, ArrangementMode secondState, float interpolationValue)
		{
			#if DEBUG
			if (float.IsNaN(interpolationValue))
			{
				throw new Exception("interpolationValue cannot be NaN");
			}
			#endif
			switch(firstState)
			{
				case  ArrangementMode.Clockwise:
					break;
				case  ArrangementMode.EvenlySpaced:
					break;
				case  ArrangementMode.Manual:
					break;
				case  ArrangementMode.CounterClockwise:
					break;
			}
			switch(secondState)
			{
				case  ArrangementMode.Clockwise:
					break;
				case  ArrangementMode.EvenlySpaced:
					break;
				case  ArrangementMode.Manual:
					break;
				case  ArrangementMode.CounterClockwise:
					break;
			}
		}
		public static void PreloadStateContent (ArrangementMode state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			object throwaway;
			switch(state)
			{
				case  ArrangementMode.Clockwise:
					break;
				case  ArrangementMode.EvenlySpaced:
					break;
				case  ArrangementMode.Manual:
					break;
				case  ArrangementMode.CounterClockwise:
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

    }
	
	
	// Extra classes
	public static class CircularLayoutManagerExtensionMethods
	{
		public static void SetVisible (this PositionedObjectList<CircularLayoutManager> list, bool value)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Visible = value;
			}
		}
	}
	
}
