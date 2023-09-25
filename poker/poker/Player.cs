using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    internal class Player
    {
        public string name { get; set; }
        public List<card> cards = new List<card>();
    }
}
