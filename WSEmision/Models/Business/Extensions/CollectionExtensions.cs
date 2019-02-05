using System;
using System.Collections.Generic;
using System.Linq;

namespace WSEmision.Models.Business.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Implementación de <see cref="List{T}.FindIndex(int, Predicate{T})"/> para
        /// la interfaz <see cref="IList{T}"/>. Si se encuentra el elemento indicado
        /// regresa su índice. De lo contrario, regresa -1.
        /// </summary>
        /// <typeparam name="T">El tipo de dato de los elementos de la lista.</typeparam>
        /// <param name="lista">La lista a buscar.</param>
        /// <param name="predicado">Función con la cual se buscará al elemento en la lista.</param>
        /// <param name="inicio">El índice a partir del cual se empezará a buscar. Por defecto
        ///  empieza desde el inicio de la lista [0].</param>
        /// <returns>El índice del elemento a buscar. Si el elemento no existe, regresa -1.</returns>
        public static int FindIndex<T>(this IList<T> lista, Func<T, bool> predicado, int inicio = 0)
        {
            for (int i = inicio; i < lista.Count; i++) {
                if (predicado(lista[i])) {
                    return i;
                }
            }
            
            return -1;
        }
    }
}