using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    internal class card
    {
        public string symbol { get; set; }
        public string color { get; set; }
        public int value { get; set; }
        
        public card(string symbol, string color, int value)
        {
            this.symbol = symbol;
            this.color = color;
            this.value = value;
        }

        public override string ToString()
        {
            return $"{symbol} {color}";
        }
        public void details()
        {
            Console.WriteLine(this);
        }
    }
}
