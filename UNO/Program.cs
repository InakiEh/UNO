using System;
using System.Collections.Generic;
using System.Linq;

namespace UNO
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Listas
            List<Carta> mazo = new List<Carta>();
            List<Carta> cementerio = new List<Carta>();
            List<Jugador> jugadores = new List<Jugador>();
            List<int> numeros = new List<int>();
            List<string> colores = new List<string>();
            List<Jugador> ordenJugadores = new List<Jugador>();
            #endregion 

            LlenarListaNumeros(numeros);
            LlenarListaColores(colores);
            CrearMazo(numeros, colores, mazo);
            mazo = mazo.OrderBy(x => Guid.NewGuid()).ToList();

            Console.WriteLine("Ingrese numero de Jugadores: ");
            int numeroDeJugadores = Int32.Parse(Console.ReadLine());

            CrearJugadores(numeroDeJugadores, jugadores);
            RepartirCartas(mazo, jugadores);
            DefinirOrdenJugadores(jugadores, ordenJugadores);
            EmpezarJuego(ordenJugadores, mazo, cementerio);
        }

        static public void LlenarListaNumeros(List<int> numeros)
        {
            for (int numero = 0; numero <= 13; numero++)
            {
                numeros.Add(numero);
            }
        }
        static public void LlenarListaColores(List<string> colores)
        {
            colores.Add("Azul");
            colores.Add("Amarillo");
            colores.Add("Verde");
            colores.Add("Rojo");
        }
        static public void CrearMazo(List<int> numeros, List<string> colores, List<Carta> Mazo)
        {
            foreach (int numero in numeros)
            {
                foreach (string color in colores)
                {
                    Carta carta = new Carta(numero, color);
                    Mazo.Add(carta);
                }
            }
            for (int numero = 1; numero < numeros.Count; numero++)
            {
                foreach (string color in colores)
                {
                    Carta carta = new Carta(numeros[numero], color);
                    Mazo.Add(carta);
                }
            }
        }
        static public void CrearJugadores(int numeroDeJugadores, List<Jugador> Jugadores)
        {
            for (int numeroDeJugador = 1; numeroDeJugador <= numeroDeJugadores; numeroDeJugador++)
            {
                List<Carta> mano = new List<Carta>();
                Jugador jugador = new Jugador(numeroDeJugador, false, mano);
                Jugadores.Add(jugador);
            }
        }
        static public void RepartirCartas(List<Carta> mazo, List<Jugador> jugadores)
        {
            foreach (Jugador jugador in jugadores)
            {
                int numeroCartasRepartidas = 0;
                foreach (Carta carta in mazo)
                {
                    jugador.mano.Add(carta);
                    numeroCartasRepartidas++;
                    if (numeroCartasRepartidas == 7)
                    {
                        int cartasEliminadas = 0;
                        foreach (Carta cartaAEliminar in mazo.ToList())
                        {
                            mazo.Remove(cartaAEliminar);
                            cartasEliminadas++;
                            if (cartasEliminadas == 7)
                            {
                                break;
                            }
                        }

                        break;
                    }
                }
            }
        }
        static public void DefinirOrdenJugadores (List<Jugador> Jugadores, List<Jugador> ordenJugadores)
        {
            foreach (Jugador jugador in Jugadores)
            {
                int resultadoDado = TirarDado();
                jugador.resultadoDado = resultadoDado;
                ordenJugadores.Add(jugador);
            }
            ordenJugadores = ordenJugadores.OrderBy(o => o.resultadoDado).Reverse().ToList();
        }
        static public int TirarDado()
        {
            var random = new Random();
            int resultadoDado = random.Next(1,7);
            return resultadoDado;
        }
        static public void EmpezarJuego(List<Jugador> ordenJugadores, List<Carta> mazo, List<Carta> cementerio)
        {
            bool primeraJugada = true;
            bool endGame = false;

            while (endGame == false){

                foreach(Jugador jugador in ordenJugadores)
                {
                    MostrarMano(jugador);
                    if (BajarPrimeraCarta(primeraJugada, cementerio, jugador))
                    {
                        primeraJugada = false;
                        continue;
                    }

                    Console.WriteLine("Cementerio :" + cementerio[cementerio.Count-1].numero + " " + cementerio[cementerio.Count - 1].color);
                    Console.WriteLine("Quiere robar una carta? S/N");
                    string respuesta = Console.ReadLine();

                    if (respuesta == "S")
                    {
                        jugador.mano.Add(mazo[0]);
                        mazo.Remove(mazo[0]);

                        bool cartaDisponible = false;

                        foreach (Carta carta in jugador.mano)
                        {
                            if (carta.numero == cementerio[cementerio.Count - 1].numero || carta.color == cementerio[cementerio.Count - 1].color)
                            {
                                cartaDisponible = true;
                            }
                        }
                        if (cartaDisponible == true)
                        {
                            MostrarMano(jugador);
                            Console.WriteLine("Cementerio :" + cementerio[cementerio.Count - 1].numero + " " + cementerio[cementerio.Count - 1].color);
                            while (true)
                            {
                                Console.WriteLine("Elige una carta para botar");
                                int cartaElegida = Int32.Parse(Console.ReadLine()) + 1;
                                if (cartaElegida < jugador.mano.Count() - 1)
                                {
                                    continue;
                                }
                                if (jugador.mano[cartaElegida].numero == cementerio[cementerio.Count - 1].numero || jugador.mano[cartaElegida].color == cementerio[cementerio.Count - 1].color)
                                {
                                    cementerio.Add(jugador.mano[cartaElegida]);
                                    jugador.mano.Remove(jugador.mano[cartaElegida]);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Carta no valida, eliga una carta correcta");
                                }
                            }
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("No tiene cartas para jugar");
                            continue;
                        }
                    }
                    else if (respuesta == "N")
                    {
                        bool cartaDisponible = false;

                        foreach (Carta carta in jugador.mano)
                        {
                            if (carta.numero == cementerio[cementerio.Count - 1].numero || carta.color == cementerio[cementerio.Count - 1].color)
                            {
                                cartaDisponible = true;
                            }
                        }
                        if (cartaDisponible == true)
                        {
                            MostrarMano(jugador);
                            Console.WriteLine("Cementerio :" + cementerio[cementerio.Count - 1].numero + " " + cementerio[cementerio.Count - 1].color);
                            while (true)
                            {
                                Console.WriteLine("Elige una carta para botar");
                                int cartaElegida = Int32.Parse(Console.ReadLine()) - 1;
                                if (cartaElegida < jugador.mano.Count() - 1)
                                {
                                    continue;
                                }
                                if (jugador.mano[cartaElegida].numero == cementerio[cementerio.Count - 1].numero || jugador.mano[cartaElegida].color == cementerio[cementerio.Count - 1].color)
                                {
                                    cementerio.Add(jugador.mano[cartaElegida]);
                                    jugador.mano.Remove(jugador.mano[cartaElegida]);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Carta no valida, eliga una carta correcta");
                                }
                            }
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("No tiene cartas para jugar");
                            continue;
                        }

                    }
                }
            }
        }
        static public void MostrarMano(Jugador jugador)
        {
            Console.WriteLine("/////MANO JUGADOR " + jugador.idJugador + "/////");
            foreach (Carta carta in jugador.mano)
            {
                Console.WriteLine(carta.numero + " " + carta.color);
            }
            Console.WriteLine("////////////////////");
        }
        static public bool BajarPrimeraCarta(bool primeraJugada, List<Carta> cementerio, Jugador jugador)
        {
            if (primeraJugada == true)
            {
                Console.WriteLine("Elige una carta para botar");
                int cartaABotar = Int32.Parse(Console.ReadLine()) - 1;
                cementerio.Add(jugador.mano[cartaABotar]);
                jugador.mano.Remove(jugador.mano[cartaABotar]);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
