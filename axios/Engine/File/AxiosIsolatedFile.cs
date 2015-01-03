using System.IO;
using System.IO.IsolatedStorage;
using Axios.Engine.Interfaces;
using System;

namespace Axios.Engine.File
{
    public class AxiosIsolatedFile : AxiosFile, IAxiosFile
    {
        protected IsolatedStorageFileStream _fs;
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

        public override Stream GetStream(FileMode mode)
        {
#if WINDOWS
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain();
#else
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();
#endif   
            _fs = null;
            _fs = savegameStorage.OpenFile(_filename, mode);
            return (Stream)_fs;
        }

        public void Dispose()
        {
            // http://msdn.microsoft.com/en-us/library/system.idisposable%28v=vs.110%29.aspx
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing && _fs != null)
            {
                _fs.Close();
            }

            disposed = true;
        }

    }
}
