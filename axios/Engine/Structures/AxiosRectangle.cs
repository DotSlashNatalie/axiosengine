using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axios.Engine.Structures
{
    class AxiosRectangle
    {
        private float _width;
        private float _height;
        private float _x;
        private float _y;

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

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

        public float Top
        {

        }

        public float Bottom
        {
            get { return _y + _height; }
        }

        public bool Intersect(AxiosRectangle rect)
        {
            bool intersects = false;



            return intersects;
        }

        public AxiosRectangle(float X, float Y, float width, float height)
        {
            _width = width;
            _height = height;
            _x = X;
            _y = Y;
        }

        
    }
}
