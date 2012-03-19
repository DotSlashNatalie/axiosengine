using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;

using Axios.Engine.Interfaces;

namespace Axios.Engine.File
{
    public class AxiosIsolatedFile : AxiosFile, IAxiosFile
    {
        
        public AxiosIsolatedFile(string filename)
        {
            this._filename = filename;
        }

        public override void WriteData(string data, FileMode mode)
        {
            //Make sure that a proper mode is passed
            if (mode == FileMode.Append
                    || mode == FileMode.Create
                    || mode == FileMode.CreateNew
                    || mode == FileMode.Truncate)
            {
#if WINDOWS
                IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain();
#else
                IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();
#endif
                IsolatedStorageFileStream fs = null;
                fs = savegameStorage.OpenFile(_filename, mode);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(data);
                sw.Close();
                this.Content = data;
            }
        }

        public override string ReadData()
        {
            string ret = "";
#if WINDOWS
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain();
#else
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();
#endif   
            IsolatedStorageFileStream fs = null;
            fs = savegameStorage.OpenFile(_filename, System.IO.FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            ret = sr.ReadToEnd();
            sr.Close();
            Content = ret;
            return ret;
        }
    }
}
