using System.IO;

namespace Axios.Engine.Interfaces
{
    interface IAxiosFile
    {
        void WriteData(string data, FileMode mode);
        string ReadData();
    }
}
