using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FarseerPhysics.SamplesFramework
{
    public class SpriteFonts
    {
        public SpriteFont DetailsFont;
        public SpriteFont FrameRateCounterFont;
        public SpriteFont MenuSpriteFont;

        public SpriteFonts(ContentManager contentManager)
        {
            MenuSpriteFont = contentManager.Load<SpriteFont>("menuFont");
            FrameRateCounterFont = contentManager.Load<SpriteFont>("frameRateCounterFont");
            DetailsFont = contentManager.Load<SpriteFont>("detailsFont");
        }
    }
}