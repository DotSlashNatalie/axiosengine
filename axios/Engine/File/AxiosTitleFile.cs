using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Axios.Engine.File;
using Axios.Engine.Interfaces;

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
    }
}
