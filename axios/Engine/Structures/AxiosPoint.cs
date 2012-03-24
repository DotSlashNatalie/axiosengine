using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axios.Engine.Structures
{
    class AxiosPoint
    {
        private float _x;
        private float _y;

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public AxiosPoint(float X, float Y)
        {
            _x = X;
            _y = Y;
        }
    }
}
