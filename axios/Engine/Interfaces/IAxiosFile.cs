using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Axios.Engine.Interfaces
{
    interface IAxiosFile
    {
        void WriteData(string data, FileMode mode);
        string ReadData();
    }
}
