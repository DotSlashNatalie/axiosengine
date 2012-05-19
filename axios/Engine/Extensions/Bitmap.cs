using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameStateManagement;

namespace Axios.Engine.Extensions
{
    public static class Bitmap_extension
    {
#if WINDOWS
        // System.Drawing is NOT avaiable on WP7 or Xbox
        /*
         * http://stackoverflow.com/a/7394185/195722
         * 
         * 
         * 
         */
        public static Texture2D GetTexture(this System.Drawing.Bitmap bitmap, GameScreen gameScreen)
        {
            BlendState oldstate = gameScreen.ScreenManager.GraphicsDevice.BlendState;
            gameScreen.ScreenManager.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Texture2D tex = new Texture2D(gameScreen.ScreenManager.GraphicsDevice, bitmap.Width, bitmap.Height, true, SurfaceFormat.Color);

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bufferSize = data.Height * data.Stride;

            //create data buffer 
            byte[] bytes = new byte[bufferSize];

            // copy bitmap data into buffer
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            // copy our buffer to the texture
            tex.SetData(bytes);

            // unlock the bitmap data
            bitmap.UnlockBits(data);

            gameScreen.ScreenManager.GraphicsDevice.BlendState = oldstate;
            return tex;
        }
#endif
    }
}
