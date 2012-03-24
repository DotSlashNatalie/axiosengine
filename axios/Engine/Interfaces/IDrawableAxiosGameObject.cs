using Microsoft.Xna.Framework;

namespace Axios.Engine.Interfaces
{
    interface IDrawableAxiosGameObject
    {
        int DrawOrder
        {
            get;
            set;
        }
        void Draw(AxiosGameScreen gameScreen, GameTime gameTime);
    }
}
