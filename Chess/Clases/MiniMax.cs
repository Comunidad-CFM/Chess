﻿using System;
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

        // Se recorre el arbol recursivamente por completo y retorna la mejor utilidad
        public double miniMax(Nodo raiz, Boolean Player)
        {
            if (raiz.hijos.Count == 0)
            {
                return raiz.utilidad;
            }

            if (Player == max) // Nivel Max
            {
                double utilidadMax = -1000000;
                foreach (Nodo hijo in raiz.hijos)
                {
                    utilidadMax = Math.Max(utilidadMax, miniMax(hijo, !Player));
                }

                return utilidadMax;
            }
            else // Nivel Min
            {
                double utilidadMin = 1000000;
                foreach (Nodo hijo in raiz.hijos)
                {
                    utilidadMin = Math.Min(utilidadMin, miniMax(hijo, !Player));
                }

                return utilidadMin;
            }
        }

        // Se obtiene la utilidad de cada hijo de la raiz
        public Nodo miniMax(Nodo raiz) 
        {
            double utilidad;

            Parallel.ForEach(raiz.hijos, nodo =>
            {
                lock (nodo)
                {
                    nodo.utilidad = miniMax(nodo, true);
                }

            });

            foreach (Nodo nodo in raiz.hijos)
            {
                utilidad = nodo.utilidad;

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
