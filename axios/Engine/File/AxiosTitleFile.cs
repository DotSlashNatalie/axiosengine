using System;
using System.IO;
using Axios.Engine.Interfaces;
using Microsoft.Xna.Framework;

namespace Axios.Engine.File
{
    public class AxiosTitleFile : AxiosFile, IAxiosFile, IDisposable
    {
        protected Stream _fs;
        public AxiosTitleFile(string filename)
        {
            //Title Files can only be opened for reading!
            
            this._filename = filename;
        }


        public override void WriteData(string data, FileMode mode)
        {
            throw new NotImplementedException();
        }

        public override string ReadData()
        {
            StreamReader sr = new StreamReader(TitleContainer.OpenStream(_filename));
            this.Content = sr.ReadToEnd();
            sr.Close();
            return this.Content;
        }

        public override Stream GetStream(FileMode mode)
        {
            _fs = (Stream)TitleContainer.OpenStream(_filename);
            return _fs;
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
