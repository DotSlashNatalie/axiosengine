using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axios.Engine.Data
{
    // Q. What is the point of this?
    // A. This is not to cache textures loaded by content manager
    //    but other data/content that isn't. Use cases include:
    //    - Any graphics generated during runtime (such as dialogs)
    //    - Any data that is loaded in during run time (such as maps)
    //    Content manager performs it's own caching so anything loaded by it
    //    or the Gameservice - then attempted to load again will not be loaded
    //    again but rather a reference to it will be returned
    //    ************************************************
    //    DANGER WILL ROBINSON DANGER
    //    ************************************************
    //    Only store stuff here that you want during the FULL lifecycle of your game
    //    The cache is never cleared - so a reference will exist for the objects you leave
    //    You MAY clear the cache by using the clear method or unset
    //
    //    You probably don't want this
    //    There is no cache...
    //    This is not the cache you are looking for...
    //
    public class Cache : Singleton<Cache>
    {
        private Dictionary<string, object> _cache;
        public Cache()
        {
            _cache = new Dictionary<string, object>();
        }

        public object get(string key)
        {
            return _cache[key];
        }

        public void set(string key, object obj)
        {
            _cache[key] = obj;
        }

        public void unset(string key)
        {
            _cache.Remove(key);
        }

        public void clear()
        {
            _cache = new Dictionary<string, object>();
        }
    }
}
