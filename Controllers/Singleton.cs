using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDll_L4_AFPE_DAVH.Controllers
{
    public sealed class Singleton
    {
        public Dictionary<string, string> fileNames = new Dictionary<string, string>();
        private readonly static Singleton _instance = new Singleton();
        private Singleton()
        {
            
        }
        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
