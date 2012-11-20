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

namespace UiTestBed.Entities.Layouts
{
	public partial class BoxLayoutManager : ILayoutable
	{
        protected List<ILayoutable> _layoutedItems;
        protected bool _redoLayout;

        public float ScaleX { get; set; }

        public float ScaleXVelocity { get; set; }

        public float ScaleY { get; set; }

        public float ScaleYVelocity { get; set; }

        public void AddItem(ILayoutable item)
        {
            if (_layoutedItems.Contains(item))
                return;

            _layoutedItems.Add(item);
            item.AttachTo(this, false);
            PerformLayout();
        }

        protected virtual void PerformLayout()
        {
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
        }

        protected virtual void PerformVerticalLayout(bool increasing)
        {
            // Calculate the width and height
            float width = 0;
            float height = 0;
            for (int x = 0; x < _layoutedItems.Count; x++)
            {
                if (x > 0)
                    height += Spacing;

                height += (_layoutedItems[x].ScaleY * 2);
                if (_layoutedItems[x].ScaleX * 2 > width)
                    width = _layoutedItems[x].ScaleX * 2;
            }

            // Add the margins
            width += (Margin * 2);
            height += (Margin * 2);

            // Set the scales
            ScaleX = (width / 2);
            ScaleY = (height / 2);

            // Compute the Scale properties
            float currentX;
            float currentY;

            if (increasing)
            {
                // bottom left corner
                currentX = this.X - ScaleX + Margin;
                currentY = this.Y - ScaleY + Margin;
            }
            else
            {
                // top left corner
                currentX = this.X - ScaleX + Margin;
                currentY = this.Y + ScaleY - Margin;
            }

            for (int x = 0; x < _layoutedItems.Count; x++)
            {
                if (x > 0)
                {
                    if (increasing)
                        currentY += Spacing;
                    else
                        currentY -= Spacing;
                }

                // Since the x/y position will point to the center, we need to account for that
                if (increasing)
                {
                    _layoutedItems[x].RelativeX = currentX + _layoutedItems[x].ScaleX;
                    _layoutedItems[x].RelativeY = currentY - _layoutedItems[x].ScaleY;

                    currentY += (_layoutedItems[x].ScaleX * 2);
                }
                else
                {
                    _layoutedItems[x].RelativeX = currentX + _layoutedItems[x].ScaleX;
                    _layoutedItems[x].RelativeY = currentY - _layoutedItems[x].ScaleY;

                    currentY -= (_layoutedItems[x].ScaleY * 2);
                }
            }
        }

        protected virtual void PerformHorizontalLayout(bool increasing)
        {
            // Calculate the width and height
            float width = 0;
            float height = 0;
            for (int x = 0; x < _layoutedItems.Count; x++)
            {
                if (x > 0)
                    width += Spacing;

                width += (_layoutedItems[x].ScaleX * 2);
                if (_layoutedItems[x].ScaleY * 2 > height)
                    height = _layoutedItems[x].ScaleY * 2;
            }

            // Add the margins
            width += (Margin * 2);
            height += (Margin * 2);

            // Set the manager's Scale properties
            ScaleX = (width / 2);
            ScaleY = (height / 2);

            // Calculate the positioning of all the controls
            float currentX;
            float currentY;

            if (increasing)
            {
                // Top left corner
                currentX = this.X - ScaleX + Margin;
                currentY = this.Y + ScaleY - Margin;
            }
            else
            {
                // Top right corner
                currentX = this.X + ScaleX - Margin;
                currentY = this.Y + ScaleY - Margin;
            }

            for (int x = 0; x < _layoutedItems.Count; x++)
            {
                if (x > 0)
                {
                    if (increasing)
                        currentX += Spacing;
                    else
                        currentX -= Spacing;
                }

                // Since the x/y position will point to the center, we need to account for that
                if (increasing)
                {
                    _layoutedItems[x].RelativeX = currentX + _layoutedItems[x].ScaleX;
                    _layoutedItems[x].RelativeY = currentY - _layoutedItems[x].ScaleY;

                    currentX += (_layoutedItems[x].ScaleX * 2);
                }
                else
                {
                    _layoutedItems[x].RelativeX = currentX - _layoutedItems[x].ScaleX;
                    _layoutedItems[x].RelativeY = currentY - _layoutedItems[x].ScaleY;

                    currentX -= (_layoutedItems[x].ScaleX * 2);
                }
            }
        }

		private void CustomInitialize()
		{
            _layoutedItems = new List<ILayoutable>();
		}

		private void CustomActivity()
		{
            if (_redoLayout)
            {
                // Reset the flag
                //_redoLayout = false;
                PerformLayout();
            }
		}

		private void CustomDestroy()
		{
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {
        } 
    }
}
