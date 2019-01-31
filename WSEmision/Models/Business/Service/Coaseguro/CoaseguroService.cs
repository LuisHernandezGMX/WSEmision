using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSEmision.Models.DAL.DAO.Coaseguro;

namespace WSEmision.Models.Business.Service.Coaseguro
{
    /// <summary>
    /// Contiene métodos de utilería para el módulo de Coaseguro de esta aplicación.
    /// </summary>
    public class CoaseguroService
    {
        /// <summary>
        /// Genera el reporte en PDF de la Cédula de Participación y el Anexo de
        /// Condiciones del Coaseguro indicado.
        /// </summary>
        /// <param name="idPv">El ID de la póliza en coaseguro.</param>
        /// <param name="rutaPlantilla">La ruta absoluta a la plantilla del reporte.</param>
        /// <returns>Los bytes del reporte generado en PDF.</returns>
        public static byte[] GenerarCedulaParticipacionAnexoCondiciones(int idPv, string rutaPlantilla)
        {
            var datosCedula = CoaseguroDao.ObtenerCedulaParticipacion(idPv);
            var datosAnexo = CoaseguroDao.ObtenerAnexoCondicionesParticulares(idPv);

            // TODO:
            
            // 2) Leer plantilla 
            // 3) Reemplazar valores en plantilla
            // 4) Escribir archivo de LaTex
            // 5) Compilar archivo
            // 6) Leer PDF resultante
            // 7) Regresar PDF como vector de bytes

            return new byte[] { };
        }
    }
}