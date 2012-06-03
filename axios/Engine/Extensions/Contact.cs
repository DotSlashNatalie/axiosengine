using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;

namespace Axios.Engine.Extensions
{
    public enum CollisionDirection
    {
        Right,
        Left,
        Top,
        Bottom
    }
    public static class AxiosExtensions_Contact
    {
        /// http://farseerphysics.codeplex.com/discussions/281783
        /// <summary>
        /// Returns the direction that the collision happened.
        /// Should be used in the event OnAfterCollision
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static CollisionDirection Direction(this Contact c)
        {
            CollisionDirection direction;
            // Work out collision direction
            Vector2 colNorm = c.Manifold.LocalNormal;
            if (Math.Abs(colNorm.X) > Math.Abs(colNorm.Y))
            {
                // X direction is dominant
                if (colNorm.X > 0)
                    direction = CollisionDirection.Right;
                else
                    direction = CollisionDirection.Left;
            }
            else
            {
                // Y direction is dominant
                if (colNorm.Y > 0)
                    direction = CollisionDirection.Bottom;
                else
                    direction = CollisionDirection.Top;
                    
            }

            return direction;
        }
    }
}
