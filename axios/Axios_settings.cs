/*
 * Axios Engine
 * 
 * By: Nathan Adams
 * 
 * CHANGELOG
 * 
 * 1.0.0.0
 * - Initial Version
 * 
 * 1.0.0.1
 * - Adding staic function SetResolution
 * 
 * 1.0.0.2
 * - Adding flag when removing object from Farseer to prevent it from getting removed twice
 * 
 * 1.0.0.3
 * - Axios.Engine.File namespace
 *      - Adding title file reading support 
 *      - Adding iosloated file storage support
 *      
 * 1.0.0.4 - 3/9/2012
 * - Condensing AddGameObject into a single method
 * 
 * 1.0.0.5 - 3/9/2012
 * - Adding checks in MenuScreen to make sure screen doesn't get struck in transition
 * 
 * 1.0.0.6 - 3/10/2012
 * - Added Singleton class
 * - Added Logging class
 * - Added LoggingFlag flags
 * - Added static loglevel setting
 * - Moving some enums out of classes
 * - Adding AxiosRegularFile class
 * 
 * 1.0.0.7 - 3/11/2012
 * - Adding IAxiosFile interface
 * 
 * 1.0.0.8 - 3/15/2012
 * - Adding code for breakable bodies
 * 
 * 1.0.0.9 - 3/16/2012
 * - Changeing the complex objects alot - now they are more like "chained" objects
 * - Adding checks for if objects are getting deleted too fast
 * 
 * 1.0.1.0 - 3/20/2012
 * - Taking out hard coded debug statements for the screen system
 * - Adding field to allow/disallow automated mouse joints per object
 * - Fixing bug with last screen not exiting if it is a background screen
 * 
 * 1.0.1.1 - 3/22/2012
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using Axios.Engine.Extenions;
using Axios.Engine.Log;

namespace Axios
{
    public enum ResolutionSetting
    {
        Windows,
        Xbox360,
        WP7_Portrait,
        WP7_Landscape
    }
    public static class Settings
    {
       

        public static LoggingFlag Loglevel = LoggingFlag.ALL;

#if WINDOWS
        public static string Version = "Axios Engine " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
#elif XBOX360 || WINDOWS_PHONE
        private static AssemblyName assemblyref = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
        public static string Version = "Axios Engine " + Settings.assemblyref.Version;
#endif

        public static bool ScreenSaver = false;

        private static ResolutionSetting _ressetting;
        /// <summary>
        /// We should have two seperate resolutions for seperate devices.
        /// This way you can have one source to preform calculations on world size depending on the device.
        /// </summary>
        public static void SetResolution(GraphicsDeviceManager graphics, ResolutionSetting setting)
        {
            //height is first
            graphics.PreferredBackBufferHeight = GetResolution(setting)[0];
            graphics.PreferredBackBufferWidth = GetResolution(setting)[1];
            _ressetting = setting;
        }

        private static int[] GetResolution(ResolutionSetting setting)
        {
            int[] screendim = new int[2];
            screendim[0] = 0;
            screendim[1] = 0;
            if (setting == ResolutionSetting.Windows || setting == ResolutionSetting.Xbox360)
            {
                screendim[0] = 720;
                screendim[1] = 1280;
            }
            
            if (setting == ResolutionSetting.WP7_Landscape)
            {
                screendim[0] = 480;
                screendim[1] = 800;
                
            } else if (setting == ResolutionSetting.WP7_Portrait)
            {
                screendim[0] = 800;
                screendim[1] = 480;
            }
        
            return screendim;
        }

        public static float GetHeightScale()
        {
            if (_ressetting == ResolutionSetting.WP7_Landscape || _ressetting == ResolutionSetting.WP7_Portrait)
            {
                return (float)GetResolution(_ressetting)[0] / (float)GetResolution(ResolutionSetting.Windows)[0];
            }
            else
            {
                return 1f;
            }
        }

        public static float GetWidthScale()
        {
            if (_ressetting == ResolutionSetting.WP7_Landscape || _ressetting == ResolutionSetting.WP7_Portrait)
            {
                return (float)GetResolution(_ressetting)[1] / (float)GetResolution(ResolutionSetting.Windows)[1];
            }
            else
            {
                return 1f;
            }
        }

        public static float GetScale()
        {
            return GetHeightScale() / GetWidthScale();
        }


        public static float DisplayUnitToSimUnitRatio = 24f;
    }
}