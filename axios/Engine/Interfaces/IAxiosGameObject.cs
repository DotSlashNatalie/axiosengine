using FarseerPhysics.SamplesFramework;
using Microsoft.Xna.Framework;
using GameStateManagement;


namespace Axios.Engine.Interfaces
{
    interface IAxiosGameObject
    {
        void Update(AxiosGameScreen gameScreen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen);
        void LoadContent(AxiosGameScreen gameScreen);
        void HandleInput(AxiosGameScreen gameScreen, InputState input, GameTime gameTime);
        void HandleCursor(AxiosGameScreen gameScreen, InputState input);
        void UnloadContent(AxiosGameScreen gameScreen);
    }
}
