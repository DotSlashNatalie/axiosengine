using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Axios.Engine.Factories
{
    public class Texture2DFactory
    {
        public static Texture2D CreateFromList(List<Texture2D> textures, int width, int height)
        {
            if (textures.Count <= 0)
                return (Texture2D)null;
            Texture2D texture2D1 = new Texture2D(textures[0].GraphicsDevice, width, height);
            Color[] data1 = new Color[width * height];
            texture2D1.GetData<Color>(data1);
            Rectangle rectangle = new Rectangle(0, 0, textures[0].Width, textures[0].Height);
            foreach (Texture2D texture2D2 in textures)
            {
                Color[] data2 = new Color[texture2D2.Width * texture2D2.Height];
                texture2D2.GetData<Color>(data2);
                texture2D1.SetData<Color>(0, new Rectangle?(rectangle), data2, 0, texture2D2.Width * texture2D2.Height);
                rectangle.X += texture2D2.Width;
                if (rectangle.X >= width)
                {
                    rectangle.X = 0;
                    rectangle.Y += texture2D2.Height;
                }
            }
            return texture2D1;
        }
    }
}
