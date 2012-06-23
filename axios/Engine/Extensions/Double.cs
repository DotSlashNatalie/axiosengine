using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axios.Engine.Extensions
{
    public static class AxiosExtension_Double
    {
        //http://www.vcskicks.com/csharp_net_angles.php
        public static double DegreeToRadian(this double angle)
        {
            return Math.PI * angle / 180.0;
        }

        //http://www.vcskicks.com/csharp_net_angles.php
        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
    }
}
