using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonBallTCG
{
    class Program
    {
        static void Main(string[] args)
        {
            ScraperDB scraper = new ScraperDB();
            scraper.Init();
        }
    }
    
}
