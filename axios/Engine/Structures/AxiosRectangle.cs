using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axios.Engine.Structures
{
    public class AxiosRectangle
    {

        private AxiosPoint _point;
        private float _width;
        private float _height;


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

        public float Top
        {
            get { return _point.Y; }
        }

        public float Right
        {
            get { return _point.X + _width; }
        }

        public float Left
        {
            get { return _point.X;  }
        }

        public float Bottom
        {
            get { return _point.Y + _height; }
        }

        /*public AxiosPoint Center
        {
            get { return new AxiosPoint(Top + (Width / 2), Bottom - (Width / 2)); }
        }*/

        public bool Intersect(AxiosRectangle rect)
        {
            //bool intersects = true;

            if (Bottom < rect.Top)
                return false;
            if (Top > rect.Bottom)
                return false;
            if (Right < rect.Left)
                return false;
            if (Left > rect.Right)
                return false;

            return true;
        }

        public AxiosRectangle(float X, float Y, float width, float height)
        {
            _width = width;
            _height = height;
            _point = new AxiosPoint(X, Y);
        }

        public override string ToString()
        {
            return String.Format("{{X:{0} Y:{1} Width:{2} Height:{3}}}", _point.X, _point.Y, Width, Height);
        }
        
    }
}
