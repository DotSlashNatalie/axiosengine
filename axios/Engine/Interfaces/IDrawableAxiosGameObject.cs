using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
