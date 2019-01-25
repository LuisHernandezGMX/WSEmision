using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WSEmision.Controllers
{
    public class Ejemplo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CoaseguroWSController : ApiController
    {
        Ejemplo[] ejemplos = new[] {
            new Ejemplo { Id = 1, Nombre = "Uno" },
            new Ejemplo { Id = 2, Nombre = "Dos" },
            new Ejemplo { Id = 3, Nombre = "Tres" },
            new Ejemplo { Id = 4, Nombre = "Cuatro" }
        };

        [HttpGet]
        public IEnumerable<Ejemplo> ObtenerEjemplos()
        {
            return ejemplos;
        }

        [HttpGet]
        public IHttpActionResult ObtenerEjemplo(int id)
        {
            var ejemplo = ejemplos.FirstOrDefault(ej => ej.Id == id);

            if (ejemplo == null) {
                return NotFound();
            }

            return Ok(ejemplo);
        }
    }
}