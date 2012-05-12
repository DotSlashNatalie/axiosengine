using System.IO;

using Axios.Engine.Interfaces;

namespace Axios.Engine.File
{
    public class AxiosRegularFile : AxiosFile, IAxiosFile
    {
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

        public override FileStream GetStream(FileMode mode)
        {
            FileStream fs = new FileStream(_filename, mode);
            return fs;
        }
    }
}
