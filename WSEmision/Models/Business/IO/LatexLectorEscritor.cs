using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace WSEmision.Models.Business.IO
{
    /// <summary>
    /// Lee plantillas de LaTex (.tex) y genera
    /// los reportes en PDF correspondientes.
    /// </summary>
    public abstract class LatexLectorEscritor : ILectorReporte, IEscritorReporte<IList<string>>
    {
        /// <summary>
        /// La ruta absoluta al compilador (.exe) de LaTex.
        /// </summary>
        protected string rutaCompilador;

        /// <summary>
        /// El directorio de entrada. Todos los archivos externos
        /// de la plantilla .tex (imágenes, referencias, etc.), incluyendo
        /// a la misma, deben estar en este directorio.
        /// </summary>
        protected string inputDir;

        /// <summary>
        /// Genera una nueva instancia de esta clase.
        /// </summary>
        /// <param name="rutaCompilador">La ruta absoluta al compilador (.exe) de LaTex.</param>
        /// <param name="inputDir">La ruta del directorio de multimedia de la plantilla.</param>
        public LatexLectorEscritor(string rutaCompilador, string inputDir)
        {
            this.rutaCompilador = rutaCompilador;
            this.inputDir = inputDir;
        }

        /// <summary>
        /// Lee la plantilla en la ruta indicada y la regresa como una lista editable
        /// de cadenas, donde cada elemento es una fila de la plantilla.
        /// </summary>
        /// <param name="rutaPlantilla">La ruta completa de la plantilla a leer.</param>
        /// <returns>Todas las filas de la plantilla.</returns>
        public IList<string> LeerPlantilla(string rutaPlantilla)
        {
            return File
                .ReadLines(rutaPlantilla)
                .ToList();
        }

        /// <summary>
        /// Lee el reporte de la ruta indicada y lo regresa como
        /// un vector de bytes.
        /// </summary>
        /// <param name="directorio">La ruta del directorio donde se encuentra el reporte.</param>
        /// <param name="nombre">El nombre del reporte con extensión.</param>
        /// <returns>Un vector de <see cref="byte"/> con los contenidos del reporte.</returns>
        public byte[] LeerReporte(string directorio, string nombre = "reporte.pdf")
        {
            var rutaReporte = Path.Combine(directorio, nombre);

            return File.ReadAllBytes(rutaReporte);
        }

        /// <summary>
        /// Utiliza los contenidos de la plantilla indicada y genera el reporte en
        /// el directorio indicado con su respectivo nombre.
        /// </summary>
        /// <param name="contenido">Los contenidos del reporte.</param>
        /// <param name="outputDir">El directorio donde se escribirá el reporte.</param>
        /// <param name="nombreArchivo">El nombre del reporte a generar.</param>
        public void GenerarReporte(IList<string> contenido, string outputDir, string nombreArchivo = "reporte")
        {
            RellenarPlantilla(contenido);

            var nombrePlantilla = nombreArchivo.EndsWith(".tex")
                ? nombreArchivo
                : nombreArchivo + ".tex";

            var rutaPlantilla = Path.Combine(outputDir, nombrePlantilla);

            if (!Directory.Exists(outputDir)) {
                Directory.CreateDirectory(outputDir);
            }

            File.WriteAllLines(rutaPlantilla, contenido);

            var process = new Process();
            process.StartInfo.FileName = rutaCompilador;
            process.StartInfo.Arguments = $"--interaction=nonstopmode --include-directory={inputDir} --output-directory={outputDir} {rutaPlantilla}";
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// Rellena la plantilla con la información relevante para el reporte. Las clases que hereden
        /// de <see cref="LatexLectorEscritor"/> deberán rellenar la plantilla con sus propias variables
        /// de instancia.
        /// </summary>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        protected abstract void RellenarPlantilla(IList<string> plantilla);
    }
}