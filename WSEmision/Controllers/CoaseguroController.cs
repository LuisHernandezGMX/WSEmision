using System.Web.Mvc;

using WSEmision.Models.DAL.DAO.Coaseguro;
using WSEmision.Models.DAL.ViewModels.Coaseguro;

namespace WSEmision.Controllers
{
    public class CoaseguroController : Controller
    {
        private CoaseguroDao dao = new CoaseguroDao();

        [HttpGet]
        public ViewResult Index()
        {
            ViewBag.CodRamo = dao.ObtenerRamosComerciales();
            ViewBag.CodSucursal = dao.ObtenerSucursales();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult ConsultarPoliza(ConsultarPolizaViewModel model)
        {
            if (ModelState.IsValid) {
                if (dao.ExistePolizaCoaseguro(model)) {
                    int idPv = dao.ObtenerIdPv(model);
                    var tipoMovimiento = dao.ObtenerTipoMovimiento(idPv);

                    if (tipoMovimiento < 2M || tipoMovimiento > 3M) {
                        ModelState.AddModelError("ValidacionGeneral", "Esta póliza no se encuentra en coaseguro.");
                    } else {
                        ViewBag.IdPv = idPv;
                        ViewBag.TipoMovimiento = tipoMovimiento;
                        ViewBag.Encabezado = dao.ObtenerEncabezado(idPv);
                        ViewBag.CedulaParticipacion = dao.ObtenerCedulaParticipacion(idPv);
                        ViewBag.AnexoCondiciones = dao.ObtenerAnexoCondicionesParticulares(idPv);
                    }
                } else {
                    ModelState.AddModelError("ValidacionGeneral", "No se encontró ninguna póliza con esos parámetros.");
                }
            }

            ViewBag.CodRamo = dao.ObtenerRamosComerciales();
            ViewBag.CodSucursal = dao.ObtenerSucursales();

            return View("Index", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                dao.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}