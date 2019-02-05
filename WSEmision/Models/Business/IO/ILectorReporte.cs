using System.Collections.Generic;

namespace WSEmision.Models.Business.IO
{
    /// <summary>
    /// Provee métodos para leer plantillas y reportes
    /// generados a partir de éstas.
    /// </summary>
    public interface ILectorReporte
    {
        /// <summary>
        /// Lee la plantilla en la ruta indicada y la regresa como una lista editable
        /// de cadenas, donde cada elemento es una fila de la plantilla.
        /// </summary>
        /// <param name="rutaPlantilla">La ruta completa de la plantilla a leer.</param>
        /// <returns>Todas las filas de la plantilla.</returns>
        IList<string> LeerPlantilla(string rutaPlantilla);

        /// <summary>
        /// Lee el reporte de la ruta indicada y lo regresa como
        /// un vector de bytes.
        /// </summary>
        /// <param name="directorio">La ruta del directorio donde se encuentra el reporte.</param>
        /// <param name="nombre">El nombre del reporte con extensión.</param>
        /// <returns>Un vector de <see cref="byte"/> con los contenidos del reporte.</returns>
        byte[] LeerReporte(string directorio, string nombre);
    }
}