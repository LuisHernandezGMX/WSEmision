using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Hosting;
using System.Net.Http.Headers;
using WSEmision.Models.Business.Service.Coaseguro;

namespace WSEmision.Controllers
{
    public class CoaseguroWSController : ApiController
    {
        /// <summary>
        /// Regresa el PDF con la Cédula de Participación y el
        /// Anexo de Condiciones Particulares del la póliza con el
        /// Id indicado.
        /// </summary>
        /// <param name="idPv">El Id de la póliza de coaseguro a buscar.</param>
        /// <returns>Una respuesta HTTP con el contenido binario del PDF de
        /// los documentos especificados.</returns>
        [HttpGet]
        [ActionName("descargarCedulaYAnexo")]
        public HttpResponseMessage DescargarCedulaParticipacionAnexoCondiciones(int idPv)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var rutaPdf = HostingEnvironment.MapPath("~/Plantillas/Coaseguro/EjemploCoaseguro.pdf");
            var rutaPlantilla = HostingEnvironment.MapPath("~/Plantillas/Coaseguro/CedulaParticipacionAnexoCondiciones.tex");
            var pdf = CoaseguroService.GenerarCedulaParticipacionAnexoCondiciones(idPv, rutaPlantilla);


            response.Content = new StreamContent(new FileStream(rutaPdf, FileMode.Open));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "CedulaAnexo_" + idPv };

            return response;
        }
    }
}