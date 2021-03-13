using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TreeSize.Services
{
    internal class AsyncDataService : IAsyncDataService
    {
        private const int __SleepTime = 3000;

        public string GetResult(DateTime Time)
        {
            Thread.Sleep(__SleepTime);

            return $"Result value {Time}";
        }
    }
}
