using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class MiniMax
    {
        public double utilidadMin = 1000000,
                      utilidadMax = -1000000;
        public bool max = true;

        public MiniMax()
        { }

        public double miniMax2(Nodo raiz, Boolean Player)
        {
            if (raiz.hijos.Count == 0)
            {
                return raiz.utilidad;
            }

            if (Player == max)
            {
                foreach (Nodo hijo in raiz.hijos)
                {
                    utilidadMax = Math.Max(utilidadMax, miniMax2(hijo, !Player));
                }

                return utilidadMax;
            }
            else
            {
                foreach (Nodo hijo in raiz.hijos)
                {
                    utilidadMin = Math.Min(utilidadMin, miniMax2(hijo, !Player));
                }

                return utilidadMin;
            }
        }

        //public Nodo miniMax2(Nodo raiz, bool Player)
        //{
        //    if (raiz.hijos.Count == 0)
        //    {
        //        return raiz;
        //    }

        //    if (Player == max)
        //    {
        //        Nodo hijoAlpha = new Nodo(raiz.tablero, utilidadMin);
        //        Nodo hijoAlpha2;
        //        foreach (Nodo hijo in raiz.hijos)
        //        {
        //            hijoAlpha2 = miniMax2(hijo, !Player);
        //            if (hijoAlpha2.utilidad > hijoAlpha.utilidad)
        //            {
        //                hijoAlpha = hijoAlpha2;
        //                hijoAlpha.tablero = hijo.tablero;
        //            }
        //        }

        //        return hijoAlpha;
        //    }
        //    else
        //    {
        //        Nodo hijoBeta = new Nodo(raiz.tablero, utilidadMax);
        //        Nodo hijoBeta2;
        //        foreach (Nodo hijo in raiz.hijos)
        //        {
        //            hijoBeta2 = miniMax2(hijo, !Player);
        //            if (hijoBeta2.utilidad < hijoBeta.utilidad)
        //            {
        //                hijoBeta = hijoBeta2;
        //                hijoBeta.tablero = hijo.tablero;
        //            }
        //        }

        //        return hijoBeta;
        //    }
        //}

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
