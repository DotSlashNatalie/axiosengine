using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;

namespace Axios.Engine.Extensions
{
    public static class MoustStateExtensions
    {
        public static Vector2 Position(this MouseState input)
        {
            return new Vector2(input.X, input.Y);
        }
    }
}
