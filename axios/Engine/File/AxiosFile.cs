using System;
using System.IO;

using Axios.Engine.Interfaces;

namespace Axios.Engine.File
{
    public class AxiosFile : IAxiosFile
    {
        protected string _content;

        public String Content
        {
            get { return _content; }
            protected set { this._content = value; }
        }

        protected string _filename;



        public virtual void WriteData(string data, FileMode mode)
        {
            throw new NotImplementedException();
        }

        public virtual string ReadData()
        {
            throw new NotImplementedException();
        }

        public virtual Stream GetStream(FileMode mode)
        {
            throw new NotImplementedException();
        }
    }
}
