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
	public partial class BoxLayoutManager : PositionedObject, IDestroyable, IVisible
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
		public enum Direction
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Up = 2, 
			Down = 3, 
			Left = 4, 
			Right = 5
		}
		protected int mCurrentDirectionState = 0;
		public Direction CurrentDirectionState
		{
			get
			{
				if (Enum.IsDefined(typeof(Direction), mCurrentDirectionState))
				{
					return (Direction)mCurrentDirectionState;
				}
				else
				{
					return Direction.Unknown;
				}
			}
			set
			{
				mCurrentDirectionState = (int)value;
				switch(CurrentDirectionState)
				{
					case  Direction.Uninitialized:
						break;
					case  Direction.Unknown:
						break;
					case  Direction.Up:
						break;
					case  Direction.Down:
						break;
					case  Direction.Left:
						break;
					case  Direction.Right:
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		
		private FlatRedBall.Sprite SpriteFrameInstance;
		public event EventHandler BeforeSpacingSet;
		public event EventHandler AfterSpacingSet;
		float mSpacing = 2f;
		public virtual float Spacing
		{
			set
			{
				if (BeforeSpacingSet != null)
				{
					BeforeSpacingSet(this, null);
				}
				mSpacing = value;
				if (AfterSpacingSet != null)
				{
					AfterSpacingSet(this, null);
				}
			}
			get
			{
				return mSpacing;
			}
		}
		public event EventHandler BeforeMarginSet;
		public event EventHandler AfterMarginSet;
		float mMargin = 0f;
		public virtual float Margin
		{
			set
			{
				if (BeforeMarginSet != null)
				{
					BeforeMarginSet(this, null);
				}
				mMargin = value;
				if (AfterMarginSet != null)
				{
					AfterMarginSet(this, null);
				}
			}
			get
			{
				return mMargin;
			}
		}
		public FlatRedBall.Graphics.Animation.AnimationChainList AnimationChains
		{
			get
			{
				return SpriteFrameInstance.AnimationChains;
			}
			set
			{
				SpriteFrameInstance.AnimationChains = value;
			}
		}
		public string CurrentChainName
		{
			get
			{
				return SpriteFrameInstance.CurrentChainName;
			}
			set
			{
				SpriteFrameInstance.CurrentChainName = value;
			}
		}
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

        public BoxLayoutManager(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public BoxLayoutManager(string contentManagerName, bool addToManagers) :
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
			SpriteFrameInstance = new FlatRedBall.Sprite();
			this.AfterVisibleSet += OnAfterVisibleSet;
			this.AfterSpacingSet += OnAfterSpacingSet;
			this.AfterMarginSet += OnAfterMarginSet;
			
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
			
			if (SpriteFrameInstance != null)
			{
				SpriteFrameInstance.Detach(); SpriteManager.RemoveSprite(SpriteFrameInstance);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (SpriteFrameInstance!= null && SpriteFrameInstance.Parent == null)
			{
				SpriteFrameInstance.CopyAbsoluteToRelative();
				SpriteFrameInstance.AttachTo(this, false);
			}
			SpriteFrameInstance.ColorOperation = FlatRedBall.Graphics.ColorOperation.Color;
			SpriteFrameInstance.PixelSize = 0.5f;
			SpriteFrameInstance.Red = 0f;
			SpriteFrameInstance.ScaleX = 32f;
			SpriteFrameInstance.ScaleY = 32f;
			SpriteFrameInstance.Visible = false;
			if (SpriteFrameInstance.Parent == null)
			{
				SpriteFrameInstance.X = 5f;
			}
			else
			{
				SpriteFrameInstance.RelativeX = 5f;
			}
			X = 0f;
			Y = 0f;
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
			SpriteManager.AddToLayer(SpriteFrameInstance, layerToAddTo);
			SpriteFrameInstance.ColorOperation = FlatRedBall.Graphics.ColorOperation.Color;
			SpriteFrameInstance.PixelSize = 0.5f;
			SpriteFrameInstance.Red = 0f;
			SpriteFrameInstance.ScaleX = 32f;
			SpriteFrameInstance.ScaleY = 32f;
			SpriteFrameInstance.Visible = false;
			if (SpriteFrameInstance.Parent == null)
			{
				SpriteFrameInstance.X = 5f;
			}
			else
			{
				SpriteFrameInstance.RelativeX = 5f;
			}
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
			SpriteManager.ConvertToManuallyUpdated(SpriteFrameInstance);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("BoxLayoutManagerStaticUnload", UnloadStaticContent);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("BoxLayoutManagerStaticUnload", UnloadStaticContent);
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
		public Instruction InterpolateToState (Direction stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  Direction.Up:
					break;
				case  Direction.Down:
					break;
				case  Direction.Left:
					break;
				case  Direction.Right:
					break;
			}
			var instruction = new DelegateInstruction<Direction>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = TimeManager.CurrentTime + secondsToTake;
			this.Instructions.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (Direction stateToStop)
		{
			switch(stateToStop)
			{
				case  Direction.Up:
					break;
				case  Direction.Down:
					break;
				case  Direction.Left:
					break;
				case  Direction.Right:
					break;
			}
			CurrentDirectionState = stateToStop;
		}
		public void InterpolateBetween (Direction firstState, Direction secondState, float interpolationValue)
		{
			#if DEBUG
			if (float.IsNaN(interpolationValue))
			{
				throw new Exception("interpolationValue cannot be NaN");
			}
			#endif
			switch(firstState)
			{
				case  Direction.Up:
					break;
				case  Direction.Down:
					break;
				case  Direction.Left:
					break;
				case  Direction.Right:
					break;
			}
			switch(secondState)
			{
				case  Direction.Up:
					break;
				case  Direction.Down:
					break;
				case  Direction.Left:
					break;
				case  Direction.Right:
					break;
			}
		}
		public static void PreloadStateContent (Direction state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			object throwaway;
			switch(state)
			{
				case  Direction.Up:
					break;
				case  Direction.Down:
					break;
				case  Direction.Left:
					break;
				case  Direction.Right:
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
			InstructionManager.IgnorePausingFor(SpriteFrameInstance);
		}

    }
	
	
	// Extra classes
	public static class BoxLayoutManagerExtensionMethods
	{
		public static void SetVisible (this PositionedObjectList<BoxLayoutManager> list, bool value)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Visible = value;
			}
		}
	}
	
}
