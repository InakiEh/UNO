using System;
using System.Collections.Generic;
using System.Text;

namespace UNO
{
    class Carta
    {
        
        public int numero { get; set; }
        public string color { get; set; }

        public Carta(int numeroCarta, string colorCarta)
        {
            numero = numeroCarta;
            color = colorCarta;
        }
    }
}
