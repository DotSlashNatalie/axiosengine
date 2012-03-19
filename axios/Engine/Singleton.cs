using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axios.Engine
{
    //http://ralch.wordpress.com/2008/11/22/the-singleton-pattern-how-to-make-it-reusable/
    //http://msdn.microsoft.com/en-us/library/ff650316.aspx
    public abstract class Singleton<T> where T: new()
    {
        private static T instance;
        private static object syncRoot = new Object();
        
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new T();
                    }
                }

                return instance;
            }
        }
    }
}
