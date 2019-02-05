namespace WSEmision.Models.Business.IO
{
    /// <summary>
    /// Provee métodos para escribir los contenidos
    /// de un reporte.
    /// </summary>
    /// <typeparam name="T">El tipo de dato de los contenidos del reporte en memoria.</typeparam>
    public interface IEscritorReporte<T>
    {
        /// <summary>
        /// Utiliza los contenidos de la plantilla indicada y genera el reporte en
        /// el directorio indicado con su respectivo nombre.
        /// </summary>
        /// <param name="contenido">Los contenidos del reporte.</param>
        /// <param name="outputDir">El directorio donde se escribirá el reporte.</param>
        /// <param name="nombreArchivo">El nombre del reporte a generar.</param>
        void GenerarReporte(T contenido, string outputDir, string nombreArchivo);
    }
}