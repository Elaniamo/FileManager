using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Services
{
    internal interface IAsyncDataService
    {
        string GetResult(DateTime Time);
    }
}
