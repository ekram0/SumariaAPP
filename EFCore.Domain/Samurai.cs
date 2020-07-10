using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Domain
{
    public class Samurai
    {
        public Samurai()
        {
            SamuraiBattles = new List<SamuraiBattle>();
            Quotes = new List<Quote>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public Clan Clan { get; set; }
        public List<SamuraiBattle> SamuraiBattles { get; set; }
        public Horse Horseefcore { get; set; }
        
    }
}
