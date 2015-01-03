using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gleed2D.InGame;
using FarseerPhysics.SamplesFramework;

namespace Axios.Engine.Extensions
{
    public static class AxiosExtensions_Vector2
    {
        public static Vector2 toSimUnits(this Vector2 vec)
        {
            return ConvertUnits.ToSimUnits(vec);
        }

        public static Vector2 toDisplayUnits(this Vector2 vec)
        {
            return ConvertUnits.ToDisplayUnits(vec);
        }

    }
}
