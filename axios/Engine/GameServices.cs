using Microsoft.Xna.Framework;

namespace Axios.Engine
{
    public static class GameServices
    {
        private static GameServiceContainer container;
        private static object lockobj;
        public static GameServiceContainer Instance
        {
            get
            {
                lock (GameServices.lockobj)
                {
                    if (container == null)
                    {
                        container = new GameServiceContainer();
                    }
                }
                return container;
            }
        }

        public static T GetService<T>()
        {
            return (T)Instance.GetService(typeof(T));
        }

        public static void AddService<T>(T service)
        {
            Instance.AddService(typeof(T), service);
        }

        public static void RemoveService<T>()
        {
            Instance.RemoveService(typeof(T));
        }
    }
}
