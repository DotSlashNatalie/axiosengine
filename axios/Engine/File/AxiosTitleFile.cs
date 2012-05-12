using System;
using System.IO;
using Axios.Engine.Interfaces;
using Microsoft.Xna.Framework;

namespace Axios.Engine.File
{
    public class AxiosTitleFile : AxiosFile, IAxiosFile
    {
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

        public override FileStream GetStream(FileMode mode)
        {
            FileStream fs = (FileStream)TitleContainer.OpenStream(_filename);
            return fs;
        }
    }
}
