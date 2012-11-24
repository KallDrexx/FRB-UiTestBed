using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using UiTestBed.Entities.Interfaces;
using UiTestBed.Data;

namespace UiTestBed.Entities.Layouts
{
	public partial class SimpleLayoutManager : ILayoutable
	{
        protected Dictionary<ILayoutable, Positioning> _items;
        protected bool _isFullScreen;
        private float _width;
        private float _height;

        public bool FullScreen
        {
            get { return _isFullScreen; }
            set
            {
                if (_isFullScreen == value)
                    return; // No change needed

                if (!_isFullScreen)
                {
                    AttachTo(SpriteManager.Camera, false);
                    RelativeX = 0;
                    RelativeY = 0;
                    RelativeZ = -40;
                    ScaleX = LayerProvidedByContainer.LayerCameraSettings.OrthogonalWidth / 2;
                    ScaleY = LayerProvidedByContainer.LayerCameraSettings.OrthogonalHeight / 2;
                }
                else
                {
                    Detach();
                }
            }
        }

        public ILayoutableEvent OnSizeChangeHandler { get; set; }
        public float ScaleXVelocity { get; set; }
        public float ScaleYVelocity { get; set; }

        public float ScaleX
        {
            get { return _width / 2; }
            set
            {
                float oldWidth = _width;
                _width = value * 2;

                if (OnSizeChangeHandler != null && oldWidth != _width)
                    OnSizeChangeHandler(this);
            }
        }

        public float ScaleY
        {
            get { return _height / 2; }
            set
            {
                float oldHeight = _height;
                _height = value * 2;

                if (OnSizeChangeHandler != null && oldHeight != _height)
                    OnSizeChangeHandler(this);
            }
        }

        public void AddItem(ILayoutable item, HorizontalPosition horizontalPosition, VerticalPosition verticalPosition, LayoutOrigin layoutFrom = LayoutOrigin.Center)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var position = new Positioning { HorizontalPosition = horizontalPosition, VerticalPosition = verticalPosition };
            _items.Add(item, position);
            item.AttachTo(this, false);

            PositionItem(item, horizontalPosition, verticalPosition, layoutFrom);

            // When the size changes, make sure to reposition the item so it's still in the same spot
            item.OnSizeChangeHandler = new ILayoutableEvent(delegate(ILayoutable sender) { PositionItem(item, horizontalPosition, verticalPosition, layoutFrom); });
        }

        private void PositionItem(ILayoutable item, HorizontalPosition horizontalPosition, VerticalPosition verticalPosition, LayoutOrigin layoutFrom)
        {
            // Make sure this is still the item's parent
            if (item.Parent != this)
            {
                if (_items.ContainsKey(item))
                    _items.Remove(item);

                return;
            }

            // Position the item correctly
            float posX;
            float posY;

            // Figure out starting horizontal and vertical coordinates
            if (horizontalPosition.Alignment == HorizontalPosition.PositionAlignment.Left)
            {
                posX = 0 - ScaleX;
            }
            else if (horizontalPosition.Alignment == HorizontalPosition.PositionAlignment.Right)
            {
                posX = ScaleX;
            }
            else
            {
                posX = 0;
            }

            if (verticalPosition.Alignment == VerticalPosition.PositionAlignment.Top)
            {
                posY = ScaleY;
            }
            else if (verticalPosition.Alignment == VerticalPosition.PositionAlignment.Bottom)
            {
                posY = 0 - ScaleY;
            }
            else
            {
                posY = 0;
            }

            // Calculate offsets
            if (horizontalPosition.OffsetIsPercentage)
            {
                var total = ScaleX * 2;
                posX += (horizontalPosition.Offset * total);
            }
            else
            {
                posX += horizontalPosition.Offset;
            }

            if (verticalPosition.OffsetIsPercentage)
            {
                var total = ScaleY * 2;
                posY += (verticalPosition.Offset * total);
            }
            else
            {
                posY += verticalPosition.Offset;
            }

            // Adjust for the specified layout origin
            switch (layoutFrom)
            {
                case LayoutOrigin.TopLeft:
                    posX += item.ScaleX;
                    posY -= item.ScaleY;
                    break;

                case LayoutOrigin.TopRight:
                    posX -= item.ScaleX;
                    posY -= item.ScaleY;
                    break;

                case LayoutOrigin.BottomLeft:
                    posX += item.ScaleX;
                    posY += item.ScaleY;
                    break;

                case LayoutOrigin.BottomRight:
                    posX -= item.ScaleX;
                    posY += item.ScaleY;
                    break;

                case LayoutOrigin.Center:
                default:
                    // PosX and PosY remain at center
                    break;
            }

            // Set the item's position to the calculated spot
            item.RelativeX = posX;
            item.RelativeY = posY;
        }

		private void CustomInitialize()
		{
            _items = new Dictionary<ILayoutable, Positioning>();
		}

		private void CustomActivity()
		{

		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
    }
}
