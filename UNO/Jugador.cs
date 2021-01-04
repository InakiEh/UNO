using System;
using System.Collections.Generic;
using System.Text;

namespace UNO
{
    class Jugador
    {
        public int idJugador { get; set; }
        public bool turno { get; set; }
        public List<Carta> mano { get; set; }
        public int resultadoDado { get; set; }
        public Jugador(int idJugadorNuevo, bool turnoJugadorNuevo, List<Carta> manoJugadorNuevo)
        {
            idJugador = idJugadorNuevo;
            turno = turnoJugadorNuevo;
            mano = manoJugadorNuevo;
        }

    }
}
