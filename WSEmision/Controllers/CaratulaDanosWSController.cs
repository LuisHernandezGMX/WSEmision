using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace WSEmision.Controllers
{
    public class CaratulaDanosWSController : ApiController
    {
        /// <summary>
        /// Regresa el PDF con la carátula de daños de la póliza
        /// con el Id indicado.
        /// </summary>
        /// <param name="idPv">El Id de la póliza de coaseguro a buscar.</param>
        /// <returns>Una respuesta HTTP con el contenido binario del PDF de
        /// los documentos especificados.</returns>
        [HttpGet]
        [ActionName("descargarCaratula")]
        public HttpResponseMessage DescargarCaratula(int idPv)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var rutaPlantilla = HostingEnvironment.MapPath("~/Plantillas/CaratulaDA/CaratulaDA.tex");

            return response;
        }
    }
}
