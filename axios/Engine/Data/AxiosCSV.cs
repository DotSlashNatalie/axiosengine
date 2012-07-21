using System.Collections.Generic;
using Axios.Engine.File;

namespace Axios.Engine.Data
{
    class AxiosCSV
    {
        private AxiosFile _file;
        public AxiosCSV(AxiosFile file)
        {
            _file = file;
        }

        public List<Dictionary<string, string>> GetData()
        {
            List<Dictionary<string, string>> ret = new List<Dictionary<string, string>>();

            return ret;
        }
    }
}
