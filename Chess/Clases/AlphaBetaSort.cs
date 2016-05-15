using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Clases
{
    // Algoritmo alpha-beta-sort
    // Busqueda por niveles(max, min), en un arbol de la mejor hoja segun la utilidad.
    // Tambien utiliza poda en la busqueda y en las hojas se escoje la mejor hoja segun el nivel sin necesidad de evaluarlas todas
    class AlphaBetaSort
    {
        public double alphaAux,
                betaAux;

        public AlphaBetaSort()
        {
            this.alphaAux = 0;
            this.betaAux = 0;
        }

        // Evalua el nivel Max
        public double AlphaSort(Nodo raiz, double alpha, double beta) // Max
        {
            if (raiz.hijos.ElementAt(0).hijos.Count == 0)
            {
                double acumulado = mejorHoja(raiz, false);
                return acumulado;
            }

            foreach (Nodo hijo in raiz.hijos)
            {
                alphaAux = BetaSort(hijo, alpha, beta);
                if (alphaAux >= beta)
                    return beta;
                if (alphaAux > alpha)
                    alpha = alphaAux;
            }
            return alpha;
        }

        // Evalua el nivel Min
        public double BetaSort(Nodo raiz, double alpha, double beta) // Min
        {
            if (raiz.hijos.ElementAt(0).hijos.Count == 0)
            {
                double utilidad = mejorHoja(raiz, true);
                return utilidad;
            }

            foreach (Nodo hijo in raiz.hijos)
            {
                betaAux = AlphaSort(hijo, alpha, beta);
                if (betaAux <= alpha)
                    return alpha;
                if (betaAux < beta)
                    beta = betaAux;
            }
            return beta;
        }

        // Retorna la mejor utilidad de las hojas segun el nivel
        public double mejorHoja(Nodo padre, Boolean jugador)
        {
            padre.hijos = padre.hijos.OrderBy(x => x.utilidad).ToList(); // Ordena los hijos ascendentemente tomando en cuenta la utilidad

            if (jugador)    // Max
                return padre.hijos.ElementAt(padre.hijos.Count - 1).utilidad; // retorna al hijo con mayor utilidad
            else            // Min
                return padre.hijos.ElementAt(0).utilidad; // retorna al hijo con menor utilidad utilidad
        }

        // Reduce el numero de nodos evaluados cuando se encuentra con una peores posibilidades que las previamente evaluadas
        // Se obtiene la utilidad de cada hijo de la raiz
        public Nodo alphaBetaSort(Nodo raiz)
        {
            double utilidad;
            foreach (Nodo nodo in raiz.hijos)
            {
                utilidad = AlphaSort(nodo, -1000, 1000);

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