using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gleed2D.InGame;
using FarseerPhysics.SamplesFramework;

namespace Axios.Engine.Extensions
{
    public static class AxiosExtensions_RectangleItemProperties
    {
        public static Vector2 getSimPosition(this RectangleItemProperties prop)
        {
            Vector2 pos = ConvertUnits.ToSimUnits(prop.Position);
            pos.X += ConvertUnits.ToSimUnits(prop.Width / 2);
            pos.Y += ConvertUnits.ToSimUnits(prop.Height / 2);
            return pos;
        }
    }
}
