using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
using FrbUi;
using FlatRedBall.Graphics;

namespace UiTestBed.Entities.Layouts
{
	public partial class BoxLayoutManager : ILayoutable
	{
        public enum Alignment { Default, Inverse }

        protected Dictionary<ILayoutable, Alignment> _items;
        protected bool _recalculateLayout;
        protected AxisAlignedRectangle _border;
        private float _width;
        private float _height;

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

        public bool ShowBorder
        {
            get { return _border != null; }
            set
            {
                if (value)
                {
                    _border = ShapeManager.AddAxisAlignedRectangle();
                    _border.AttachTo(this, false);
                    _border.ScaleX = ScaleX;
                    _border.ScaleY = ScaleY;
                }
                else
                {
                    ShapeManager.Remove(_border);
                    _border = null;
                }
            }
        }

        public Layer Layer { get { return LayerProvidedByContainer; } }
        public ILayoutableEvent OnSizeChangeHandler { get; set; }
        public float ScaleYVelocity { get; set; }
        public float ScaleXVelocity { get; set; }

        public void AddItem(ILayoutable item, bool inverseAlignment = false)
        {
            if (_items.ContainsKey(item))
                return;

            var alignment = (inverseAlignment ? Alignment.Inverse : Alignment.Default);
            _items.Add(item, alignment);
            item.AttachTo(this, false);
            _recalculateLayout = true;
            PerformLayout();

            item.OnSizeChangeHandler = new ILayoutableEvent(delegate(ILayoutable sender) 
            { 
                _recalculateLayout = true; 
            });
        }

        public override void UpdateDependencies(double currentTime)
        {
            PerformLayout();
            base.UpdateDependencies(currentTime);
        }

        public override void ForceUpdateDependencies()
        {
            PerformLayout();
            base.ForceUpdateDependencies();
        }

        protected virtual void PerformLayout()
        {
            // Remove any items that this is no longer the parent of
            for (int x = _items.Count - 1; x >= 0; x--)
            {
                var item = _items.Keys.ElementAt(x);
                if (item.Parent != this)
                {
                    _items.Remove(item);
                    _recalculateLayout = true;
                }
            }

            if (!_recalculateLayout)
                return; // Not flagged to actually reset the layout

            // Reset the flag so we don't reset the layout again
            _recalculateLayout = false;

            switch (CurrentDirectionState)
            {
                case Direction.Up:
                    PerformVerticalLayout(true);
                    break;

                case Direction.Down:
                    PerformVerticalLayout(false);
                    break;

                case Direction.Left:
                    PerformHorizontalLayout(false);
                    break;

                case Direction.Right:
                default:
                    PerformHorizontalLayout(true);
                    break;
            }

            if (_border != null)
            {
                _border.ScaleX = ScaleX;
                _border.ScaleY = ScaleY;
            }
        }

        protected virtual void PerformVerticalLayout(bool increasing)
        {
            // Calculate the width and height
            float width = 0;
            float height = 0;

            foreach (var item in _items.Keys)
            {
                height += (item.ScaleY);
                if (item.ScaleX > width)
                    width = item.ScaleX;
            }

            // Add the margins and spacings
            width += Margin;
            height += (Margin + ((Spacing / 2) * _items.Count - 1));

            // Set the scales
            ScaleX = (width);
            ScaleY = (height);

            // Compute the Scale properties
            float currentX, currentY;

            if (increasing)
            {
                // bottom left corner
                currentX = 0 - ScaleX + Margin;
                currentY = 0 - ScaleY + Margin;
            }
            else
            {
                // top left corner
                currentX = 0 - ScaleX + Margin;
                currentY = ScaleY - Margin;
            }

            bool firstItem = true;
            foreach (var item in _items.Keys)
            {
                if (!firstItem)
                {
                    if (increasing)
                        currentY += Spacing;
                    else
                        currentY -= Spacing;
                }

                // Since the x/y position will point to the center, we need to account for that
                if (increasing)
                {
                    if (_items[item] == Alignment.Inverse)
                        item.RelativeX = (currentX * -1) - item.ScaleX;
                    else
                        item.RelativeX = currentX + item.ScaleX;

                    item.RelativeY = currentY + item.ScaleY;
                    currentY += (item.ScaleY * 2);
                }
                else
                {
                    if (_items[item] == Alignment.Inverse)
                        item.RelativeX = (currentX * -1) - item.ScaleX;
                    else
                        item.RelativeX = currentX + item.ScaleX;
                    
                    item.RelativeY = currentY - item.ScaleY;
                    currentY -= (item.ScaleY * 2);
                }

                firstItem = false;
            }
        }

        protected virtual void PerformHorizontalLayout(bool increasing)
        {
            // Calculate the width and height
            float halfWidth = 0;
            float halfHeight = 0;
            foreach (var item in _items.Keys)
            {
                halfWidth += (item.ScaleX);
                if (item.ScaleY > halfHeight)
                    halfHeight = item.ScaleY;
            }

            // Add the margins
            halfWidth += (Margin + ((Spacing / 2) * _items.Count - 1));
            halfHeight += Margin;

            // Set the manager's Scale properties
            ScaleX = (halfWidth);
            ScaleY = (halfHeight);

            // Calculate the positioning of all the controls
            float currentX, currentY;

            if (increasing)
            {
                // Top left corner
                currentX = 0 - ScaleX + Margin;
                currentY = ScaleY - Margin;
            }
            else
            {
                // Top right corner
                currentX = ScaleX - Margin;
                currentY = ScaleY - Margin;
            }

            bool firstItem = true;
            foreach (var item in _items.Keys)
            {
                if (!firstItem)
                {
                    if (increasing)
                        currentX += Spacing;
                    else
                        currentX -= Spacing;
                }

                // Since the x/y position will point to the center, we need to account for that
                if (increasing)
                {
                    if (_items[item] == Alignment.Inverse)
                        item.RelativeY = (currentY * -1) + item.ScaleY;
                    else
                        item.RelativeY = currentY - item.ScaleY;

                    item.RelativeX = currentX + item.ScaleX;
                    currentX += (item.ScaleX * 2);
                }
                else
                {
                    if (_items[item] == Alignment.Inverse)
                        item.RelativeY = (currentY * -1) + item.ScaleY;
                    else
                        item.RelativeY = currentY - item.ScaleY;

                    item.RelativeX = currentX - item.ScaleX;
                    currentX -= (item.ScaleX * 2);
                }

                firstItem = false;
            }
        }

		private void CustomInitialize()
		{
            _items = new Dictionary<ILayoutable, Alignment>();
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
