using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Axios.Engine.Extensions
{
    public static class AxiosExtensions_InputState
    {
        // I got tired of always specifying Player one
        //-- Nathan Adams [adamsna@datanethost.net] - 6/3/2012
        /// <summary>
        /// This checks if the key is pressed by player one
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyPressed(this InputState input, Keys key)
        {
            PlayerIndex p;
            return input.IsKeyPressed(key, PlayerIndex.One, out p);
        }

        public static bool IsButtonPressed(this InputState input, Buttons button)
        {
            PlayerIndex p;
            return input.IsButtonPressed(button, PlayerIndex.One, out p);
        }

        public static bool IsNewKeyPress(this InputState input, Keys key)
        {
            PlayerIndex p;
            return input.IsNewKeyPress(key, PlayerIndex.One, out p);
        }

        public static bool IsNewButtonPress(this InputState input, Buttons button)
        {
            PlayerIndex p;
            return input.IsNewButtonPress(button, PlayerIndex.One, out p);
        }

        public static bool IsNewButtonRelease(this InputState input, Buttons button)
        {
            PlayerIndex p;
            return input.IsNewButtonRelease(button, PlayerIndex.One, out p);
        }

        public bool IsNewKeyRelease(this InputState input, Keys key)
        {
            PlayerIndex p;
            return input.IsNewKeyRelease(key, PlayerIndex.One, out p);
        }
    }
}
