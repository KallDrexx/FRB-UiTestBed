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
using Microsoft.Xna.Framework;

namespace UiTestBed.Entities.Layouts
{
	public partial class CircularLayoutManager : ILayoutable
	{
        public event ILayoutableEvent OnSizeChange;

        protected float _width;
        protected float _height;
        protected AxisAlignedRectangle _border;
        protected Dictionary<ILayoutable, CircularPosition> _items;
        protected bool _recalculateLayout;

        public float ScaleXVelocity { get; set; }
        public float ScaleYVelocity { get; set; }

        public float ScaleX
        {
            get { return _width / 2; }
            set
            {
                float prevWidth = _width;
                _width = value * 2;

                if (OnSizeChange != null && _width != prevWidth)
                    OnSizeChange(this);

                if (_border != null)
                    _border.ScaleX = value;
            }
        }

        public float ScaleY
        {
            get { return _height / 2; }
            set
            {
                float prevHeight = _height;
                _height = value * 2;

                if (OnSizeChange != null && _height != prevHeight)
                    OnSizeChange(this);

                if (_border != null)
                    _border.ScaleY = value;
            }
        }

        public bool ShowBorder
        {
            get { return _border != null; }
            set
            {
                if (value)
                {
                    if (_border == null)
                    {
                        _border = ShapeManager.AddAxisAlignedRectangle();
                        _border.AttachTo(this, false);
                        _border.ScaleX = ScaleX;
                        _border.ScaleY = ScaleY;
                    }
                }
                else
                {
                    ShapeManager.Remove(_border);
                    _border = null;
                }
            }
        }

        public void Add(ILayoutable item, float radiusOffset, float absoluteDegrees = -1)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!_items.ContainsKey(item))
            {
                // Add the item to the list
                _items.Add(item, new CircularPosition());
                item.AttachTo(this, false);
            }

            // Calculate the item's position
            _items[item].RadiusOffset = radiusOffset;

            if (absoluteDegrees >= 0)
                _items[item].Radians = MathHelper.ToRadians(absoluteDegrees);

            // Reposition the item
            PositionItem(item);
            _recalculateLayout = true;

            // Set the size to realculate when a control changes 
            item.OnSizeChange += new ILayoutableEvent(delegate(ILayoutable sender) { _recalculateLayout = true; });
        }

        public override void UpdateDependencies(double currentTime)
        {
            RecalculateLayoutSize();
            base.UpdateDependencies(currentTime);
        }

        public override void ForceUpdateDependencies()
        {
            RecalculateLayoutSize();
            base.ForceUpdateDependencies();
        }

        protected void PositionItem(ILayoutable item)
        {
            if (!_items.ContainsKey(item))
                return;

            var position = _items[item];
            float xCoord = (float)Math.Cos(position.Radians) * (Radius + position.RadiusOffset);
            float yCoord = (float)Math.Sin(position.Radians) * (Radius + position.RadiusOffset);

            item.RelativeX = xCoord;
            item.RelativeY = yCoord;
        }

        protected void RecalculateLayoutSize()
        {
            if (!_recalculateLayout)
                return;

            // Go through all the items and find the min and max positions of items
            //   The origin is always 
            float? minX = null, minY = null, maxX = null, maxY = null;

            foreach (var item in _items.Keys)
            {
                float leftX = item.RelativeX - item.ScaleX;
                float rightX = item.RelativeX + item.ScaleX;
                float topY = item.RelativeY + item.ScaleY;
                float bottomY = item.RelativeY - item.ScaleY;

                // If the item has any bounds outside of the current min/max, 
                //    set it as the new min/max
                if (minX == null || minX > leftX)
                    minX = leftX;

                if (maxX == null || maxX < rightX)
                    maxX = rightX;

                if (minY == null || minY > bottomY)
                    minY = bottomY;

                if (maxY == null || maxY < topY)
                    maxY = topY;
            }

            // Recalculate scale to whatever the maximum values are
            ScaleX = (Math.Max(Math.Abs(minX.Value), Math.Abs(maxX.Value))) + Margin;
            ScaleY = (Math.Max(Math.Abs(minY.Value), Math.Abs(maxY.Value))) + Margin;
            _recalculateLayout = false;
        }

		private void CustomInitialize()
		{
            _items = new Dictionary<ILayoutable, CircularPosition>();
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

        public class CircularPosition
        {
            public float Radians { get; set; }
            public float RadiusOffset { get; set; }
        }
    }
}