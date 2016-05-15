using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    // Algoritmo min-max
    // Busqueda por niveles(max, min), en un arbol de la mejor hoja segun la utilidad. Se evaluan todos los nodos
    class MiniMax
    {
        public bool max = true;

        public MiniMax()
        { }

        // Se recorre el arbol por completo y retorna la mejor utilidad
        public double miniMax2(Nodo raiz, Boolean Player)
        {
            if (raiz.hijos.Count == 0)
            {
                return raiz.utilidad;
            }

            if (Player == max)
            {
                double utilidadMax = -1000000;
                foreach (Nodo hijo in raiz.hijos)
                {
                    utilidadMax = Math.Max(utilidadMax, miniMax2(hijo, !Player));
                }

                return utilidadMax;
            }
            else
            {
                double utilidadMin = 1000000;
                foreach (Nodo hijo in raiz.hijos)
                {
                    utilidadMin = Math.Min(utilidadMin, miniMax2(hijo, !Player));
                }

                return utilidadMin;
            }
        }

        public Nodo miniMax(Nodo raiz) 
        {
            double utilidad;
            foreach (Nodo nodo in raiz.hijos)
            {
                utilidad = miniMax2(nodo, true);

                if (raiz.utilidad <= utilidad)
                {
                    raiz.tablero = nodo.tablero;
                    raiz.utilidad = utilidad;
                }
            }
            
            return raiz;
        }
    }
}
