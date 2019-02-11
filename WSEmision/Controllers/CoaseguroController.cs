using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WSEmision.Models.DAL.DAO.Coaseguro;
using WSEmision.Models.DAL.ViewModels.Coaseguro;


namespace WSEmision.Controllers
{
    public class CoaseguroController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            ViewBag.CodRamo = CoaseguroDao.ObtenerRamosComerciales();
            ViewBag.CodSucursal = CoaseguroDao.ObtenerSucursales();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult ConsultarPoliza(ConsultarPolizaViewModel model)
        {
            if (ModelState.IsValid) {
                if (CoaseguroDao.ExistePolizaCoaseguro(model)) {
                    int idPv = CoaseguroDao.ObtenerIdPv(model);

                    ViewBag.IdPv = idPv;
                    ViewBag.CedulaParticipacion = CoaseguroDao.ObtenerCedulaParticipacion(idPv);
                    ViewBag.AnexoCondiciones = CoaseguroDao.ObtenerAnexoCondicionesParticulares(idPv);
                } else {
                    ModelState.AddModelError("ValidacionGeneral", "No se encontró ninguna póliza con esos parámetros.");
                }
            }

            ViewBag.CodRamo = CoaseguroDao.ObtenerRamosComerciales();
            ViewBag.CodSucursal = CoaseguroDao.ObtenerSucursales();

            return View("Index", model);
        }
    }
}