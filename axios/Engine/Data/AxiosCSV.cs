using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
