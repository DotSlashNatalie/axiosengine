using System.IO;
using System;

using Axios.Engine.Interfaces;

namespace Axios.Engine.File
{
    public class AxiosRegularFile : AxiosFile, IAxiosFile, IDisposable
    {
        protected FileStream _fs;
        public AxiosRegularFile(string file)
        {
            _filename = file;
        }

        public override void WriteData(string data, FileMode mode)
        {
            //Make sure that a proper mode is passed
            if (mode == FileMode.Append
                    || mode == FileMode.Create
                    || mode == FileMode.CreateNew
                    || mode == FileMode.Truncate)
            {
                FileStream fs = new FileStream(_filename, mode);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(data);
                sw.Close();

            }
        }

        public override string ReadData()
        {
            string ret = "";
            FileStream fs = new FileStream(_filename, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            ret = sr.ReadToEnd();
            sr.Close();
            return ret;
        }

        public override Stream GetStream(FileMode mode)
        {
            _fs = new FileStream(_filename, mode);
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
