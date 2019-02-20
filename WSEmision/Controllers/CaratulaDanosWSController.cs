using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;

using WSEmision.Models.Business.Service.CaratulaDanos;

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
            var pdf = CaratulaDanosService.GenerarCaratula(idPv, rutaPlantilla);

            response.Content = new ByteArrayContent(pdf);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline") {
                FileName = $"CaratulaDanos_{idPv}_{DateTime.Now.ToString("dd-MM-yyyy")}"
            };

            return response;
        }
    }
}
