using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall.Graphics;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math;
using FlatRedBall.Graphics.Model;
using FlatRedBall;

namespace UiTestBed.Entities.Interfaces
{
    public interface ILayoutable : IVisible, IScalable, IPositionable
    {
        void AttachTo(PositionedObject obj, bool changeRelative);
        float RelativeX { get; set; }
        float RelativeY { get; set; }
    }
}
