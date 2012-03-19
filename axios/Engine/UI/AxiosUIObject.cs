using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Axios.Engine;

namespace Axios.Engine.UI
{
    public class AxiosUIObject : DrawableAxiosGameObject
    {
        public int Width
        {
            get { return this.Texture.Width; }
            private set {  }
        }

        public int Height
        {
            get { return this.Texture.Height; }
            private set {}
        }

    }
}
