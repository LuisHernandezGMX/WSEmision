using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;

using WSEmision.Models.Business.Service.Coaseguro;

namespace WSEmision.Controllers
{
    public class CoaseguroWSController : ApiController
    {
        /// <summary>
        /// Regresa el PDF con la Cédula de Participación y el
        /// Anexo de Condiciones Particulares de la póliza con el
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
            var rutaPlantilla = HostingEnvironment.MapPath("~/Plantillas/Coaseguro/CedulaParticipacionAnexoCondiciones.tex");
            var pdf = CoaseguroService.GenerarCedulaYAnexo(idPv, rutaPlantilla);

            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline") {
                FileName = $"CedulaAnexo_{idPv}_{DateTime.Now.ToString("dd-MM-yyyy")}"
            };

            return response;
        }

        /// <summary>
        /// Regresa el PDF con la Cédula de Participación
        /// de la póliza con el Id indicado.
        /// </summary>
        /// <param name="idPv">El Id de la póliza de coaseguro a buscar.</param>
        /// <returns>Una respuesta HTTP con el contenido binario del PDF del
        /// documento especificado.</returns>
        [HttpGet]
        [ActionName("descargarCedula")]
        public HttpResponseMessage DescargarCedulaParticipacion(int idPv)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var rutaPlantilla = HostingEnvironment.MapPath("~/Plantillas/Coaseguro/CedulaParticipacion.tex");
            var pdf = CoaseguroService.GenerarCedula(idPv, rutaPlantilla);

            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                FileName = $"Cedula_{idPv}_{DateTime.Now.ToString("dd-MM-yyyy")}"
            };

            return response;
        }

        /// <summary>
        /// Regresa el PDF con Anexo de Condiciones Particulares
        /// de la póliza con el Id indicado.
        /// </summary>
        /// <param name="idPv">El Id de la póliza de coaseguro a buscar.</param>
        /// <returns>Una respuesta HTTP con el contenido binario del PDF del
        /// documento especificado.</returns>
        [HttpGet]
        [ActionName("descargarAnexo")]
        public HttpResponseMessage DescargarAnexoCondiciones(int idPv)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var rutaPlantilla = HostingEnvironment.MapPath("~/Plantillas/Coaseguro/AnexoCondiciones.tex");
            var pdf = CoaseguroService.GenerarAnexo(idPv, rutaPlantilla);

            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                FileName = $"Anexo_{idPv}_{DateTime.Now.ToString("dd-MM-yyyy")}"
            };

            return response;
        }
    }
}