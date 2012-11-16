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

        public float ScaleX
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float ScaleXVelocity
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float ScaleY
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float ScaleYVelocity
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AddItem(ILayoutable item)
        {
            if (_layoutedItems.Contains(item))
                return;

            _layoutedItems.Add(item);
            item.AttachTo(this, false);
            _redoLayout = true;
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
            float currentX = Margin;
            float currentY = Margin;

            if (!increasing)
            {
                currentX *= -1;
                currentY *= -1;
            }

            foreach (var item in _layoutedItems)
            {
                // Since the x/y position will point to the center, we need to account for that
                if (increasing)
                {
                    item.RelativeX = currentX + (item.ScaleX / 2);
                    item.RelativeY = currentY + (item.ScaleY / 2) + Spacing;

                    currentY = item.RelativeY + (item.ScaleY * 2) + Spacing;
                }
                else
                {
                    item.RelativeX = currentX - (item.ScaleX / 2);
                    item.RelativeY = currentY - (item.ScaleY / 2) - Spacing;

                    currentY = item.RelativeY - (item.ScaleY * 2) - Spacing;
                }
            }
        }

        protected virtual void PerformHorizontalLayout(bool increasing)
        {
            float currentX = Margin;
            float currentY = Margin;

            if (!increasing)
            {
                currentX *= -1;
                currentY *= -1;
            }

            foreach (var item in _layoutedItems)
            {
                // Since the x/y position will point to the center, we need to account for that
                if (increasing)
                {
                    item.RelativeX = currentX + (item.ScaleX / 2) + Spacing;
                    item.RelativeY = currentY + (item.ScaleY / 2);

                    currentX = item.RelativeX + (item.ScaleX * 2) + Spacing;
                }
                else
                {
                    item.RelativeX = currentX - (item.ScaleX / 2) - Spacing;
                    item.RelativeY = currentY - (item.ScaleY / 2);

                    currentX = item.RelativeX - (item.ScaleX * 2) - Spacing;
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
