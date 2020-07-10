using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Domain
{
    public class SamuraiBattle
    {
        public int SamuriaID { get; set; }
        public int BattleID { get; set; }
        public Samurai Samuria { get; set; }
        public Battle Battle { get; set; }
    }
}
