using FarseerPhysics.SamplesFramework;
using Microsoft.Xna.Framework;


namespace Axios.Engine.Interfaces
{
    interface IAxiosGameObject
    {
        void Update(AxiosGameScreen gameScreen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen);
        void LoadContent(AxiosGameScreen gameScreen);
        void HandleInput(AxiosGameScreen gameScreen, InputHelper input, GameTime gameTime);
        void HandleCursor(AxiosGameScreen gameScreen, InputHelper input);
        void UnloadContent(AxiosGameScreen gameScreen);
    }
}
