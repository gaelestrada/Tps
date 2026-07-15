using System;
using System.Collections.Generic;
using System.Threading;


// TP1
// Realizar una simulación de copos de nieve utilizando clases,
// listas y una animación en consola.
namespace TP1
{
    class Configuracion
    {
        public int Filas = 25;
        public int Columnas = 60;
        public int Velocidad = 120;
    }
}

namespace TP1
{
    class Copo
    {
        public int Fila;
        public int Columna;

        public Copo(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        public void Mostrar()
        {
            Console.SetCursorPosition(Columna, Fila);
            Console.Write("*");
        }

        public void Borrar()
        {
            Console.SetCursorPosition(Columna, Fila);
            Console.Write(" ");
        }

        public void Bajar()
        {
            Fila++;
        }
    }
}
namespace TP1
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuracion config = new Configuracion();

            Console.CursorVisible = false;
            Console.SetWindowSize(config.Columnas, config.Filas + 1);
            Console.SetBufferSize(config.Columnas, config.Filas + 1);

            List<Copo> copos = new List<Copo>();

            Random random = new Random();

            bool[,] ocupadas = new bool[config.Filas, config.Columnas];

            while (true)
            {
                int columnaNueva = random.Next(config.Columnas);

                if (!ocupadas[0, columnaNueva])
                {
                    Copo nuevo = new Copo(0, columnaNueva);

                    copos.Add(nuevo);

                    ocupadas[0, columnaNueva] = true;
                }

                for (int i = copos.Count - 1; i >= 0; i--)
                {
                    Copo c = copos[i];

                    c.Borrar();

                    ocupadas[c.Fila, c.Columna] = false;

                    if (c.Fila < config.Filas - 1)
                    {
                        if (!ocupadas[c.Fila + 1, c.Columna])
                        {
                            c.Bajar();
                        }
                    }

                    ocupadas[c.Fila, c.Columna] = true;

                    c.Mostrar();
                }

                for (int fila = config.Filas - 1; fila >= 0; fila--)
                {
                    bool completa = true;

                    for (int columna = 0; columna < config.Columnas; columna++)
                    {
                        if (!ocupadas[fila, columna])
                        {
                            completa = false;
                        }
                    }

                    if (completa)
                    {
                        foreach (Copo c in copos)
                        {
                            if (c.Fila == fila)
                            {
                                c.Borrar();
                            }
                        }

                        copos.RemoveAll(x => x.Fila == fila);

                        foreach (Copo c in copos)
                        {
                            if (c.Fila < fila)
                            {
                                ocupadas[c.Fila, c.Columna] = false;

                                c.Bajar();

                                ocupadas[c.Fila, c.Columna] = true;
                            }
                        }
                    }
                }

                Thread.Sleep(config.Velocidad);
            }
        }
    }
}
