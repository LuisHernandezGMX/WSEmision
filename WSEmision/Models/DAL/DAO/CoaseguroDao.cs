using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

using WSEmision.Models.DAL.DTO;
using WSEmision.Models.DAL.DTO.Coaseguro;
using WSEmision.Models.DAL.Entities;
using WSEmision.Models.DAL.ViewModels.Coaseguro;

namespace WSEmision.Models.DAL.DAO.Coaseguro
{
    /// <summary>
    /// Funciones de acceso a base de datos para el módulo de Coaseguro.
    /// </summary>
    public class CoaseguroDao : IDisposable
    {
        /// <summary>
        /// El nombre del entorno de base de datos a utilizar.
        /// </summary>
        private string entorno;

        /// <summary>
        /// La conexión hacia la base de datos.
        /// </summary>
        private EmisionContext db;

        /// <summary>
        /// Genera una nueva conexión a la base de datos, tomando
        /// el entorno del archivo [web.config].
        /// </summary>
        public CoaseguroDao()
        {
            entorno = ConfigurationManager.AppSettings["EntornoBD"];
            db = new EmisionContext(entorno);
        }

        /// <summary>
        /// Ejecuta el procedimiento sp_EncabezadoReportesEmision()
        /// y regresa toda la información de éste.
        /// </summary>
        /// <param name="idPv">El Id de la póliza a buscar.</param>
        /// <returns>Una nueva instancia de <see cref="EncabezadoReportesEmisionResultSet"/>
        /// con los datos requeridos.</returns>
        public EncabezadoReportesEmisionResultSet ObtenerEncabezado(int idPv)
        {
            EncabezadoReportesEmisionResultSet rs;
            var cmd = db.Database.Connection.CreateCommand();
            var paramIdPv = cmd.CreateParameter();

            cmd.CommandText = "EXEC sp_EncabezadoReportesEmision @IdPv";
            paramIdPv.ParameterName = "@IdPv";
            paramIdPv.Value = idPv;
            cmd.Parameters.Add(paramIdPv);

            try {
                db.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                var context = (db as IObjectContextAdapter).ObjectContext;

                rs = context
                    .Translate<EncabezadoReportesEmisionResultSet>(reader)
                    .FirstOrDefault() ?? new EncabezadoReportesEmisionResultSet();
            } catch {
                // TODO: Posible Log.
                throw;
            } finally {
                db.Database.Connection.Close();
            }

            return rs;
        }

        /// <summary>
        /// Ejecuta el procedimiento sp_CedulaParticipacionCoaseguro() y regresa
        /// toda la información de éste.
        /// </summary>
        /// <param name="idPv">El Id de la póliza a buscar.</param>
        /// <returns>Una nueva instancia de <see cref="CedulaParticipacionCoaseguroResultSet"/>
        /// con los datos requeridos.</returns>
        public CedulaParticipacionCoaseguroResultSet ObtenerCedulaParticipacion(int idPv)
        {
            var rs = new CedulaParticipacionCoaseguroResultSet();
            var cmd = db.Database.Connection.CreateCommand();
            var paramIdPv = cmd.CreateParameter();

            cmd.CommandText = "EXEC sp_CedulaParticipacionCoaseguro @IdPv";
            paramIdPv.ParameterName = "@IdPv";
            paramIdPv.Value = idPv;
            cmd.Parameters.Add(paramIdPv);

            try {
                db.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                var context = (db as IObjectContextAdapter).ObjectContext;

                rs.DatosGenerales = context
                    .Translate<DatosGeneralesCedulaRS>(reader)
                    .FirstOrDefault() ?? new DatosGeneralesCedulaRS();

                reader.NextResult();
                rs.Coaseguradoras = context
                    .Translate<CoaseguradorasCedulaRS>(reader)
                    .ToList();
            } catch {
                // TODO: Posible Log.
                throw;
            } finally {
                db.Database.Connection.Close();
            }

            return rs;
        }

        /// <summary>
        /// Ejecuta el procedimiento sp_AnexoCondicionesParticularesCoaseguro() y
        /// regresa toda la información de éste.
        /// </summary>
        /// <param name="idPv">El Id de la póliza a buscar.</param>
        /// <returns>>Una nueva instancia de <see cref=""/>
        /// con los datos requeridos.</returns>
        public AnexoCondicionesParticularesCoaseguroResultSet ObtenerAnexoCondicionesParticulares(int idPv)
        {
            var rs = new AnexoCondicionesParticularesCoaseguroResultSet();
            var cmd = db.Database.Connection.CreateCommand();
            var paramIdPv = cmd.CreateParameter();

            cmd.CommandText = "EXEC sp_AnexoCondicionesParticularesCoaseguro @IdPv";
            paramIdPv.ParameterName = "@IdPv";
            paramIdPv.Value = idPv;
            cmd.Parameters.Add(paramIdPv);

            try {
                db.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                var context = (db as IObjectContextAdapter).ObjectContext;

                rs.DatosGenerales = context
                    .Translate<DatosGeneralesAnexoRS>(reader)
                    .FirstOrDefault() ?? new DatosGeneralesAnexoRS();

                // *** Posibles efectos secundarios
                // Translate<T>() por alguna razón no obtiene el campo 'Vigencia' del DbDataReader.
                // Por lo tanto, se obtiene el valor manualmente y se asigna al result set.
                rs.DatosGenerales.FechaVigencia = reader["Vigencia"] as DateTime?;

                reader.NextResult();
                rs.GMX = context
                    .Translate<GMXAnexoRS>(reader)
                    .FirstOrDefault() ?? new GMXAnexoRS();

                reader.NextResult();
                rs.Coaseguradoras = context
                    .Translate<CoaseguradorasAnexoRS>(reader)
                    .ToList();

                reader.NextResult();
                rs.DatosEspecificos = context
                    .Translate<DatosEspecificosAnexoRS>(reader)
                    .FirstOrDefault() ?? new DatosEspecificosAnexoRS();
            } catch {
                // TODO: Posible log
                throw;
            } finally {
                db.Database.Connection.Close();
            }

            return rs;
        }

        /// <summary>
        /// Regresa una lista de los ramos comerciales (1 - 102) para
        /// ser incluida en una etiqueta HTML &lt;select&gt;.
        /// </summary>
        /// <returns>Una colección de objectos <see cref="SelectListItem"/></returns>
        public IEnumerable<SelectListItem> ObtenerRamosComerciales()
        {
            return db.tramo
                .Where(ramo => ramo.cod_ramo <= 102M)
                .AsEnumerable()
                .Select(ramo => new SelectListItem {
                    Text = $"{ramo.txt_desc} ({ramo.cod_ramo})",
                    Value = ramo.cod_ramo.ToString()
                })
                .ToList();
        }

        /// <summary>
        /// Regresa una lista con todas las sucursales de GMX para ser incluida
        /// en una etiqueta HTML &lt;select&gt;.
        /// </summary>
        /// <returns>Una colección de objetos <see cref="SelectListItem"/></returns>
        public IEnumerable<SelectListItem> ObtenerSucursales()
        {
            return db.tsuc
                .AsEnumerable()
                .Select(tsuc => new SelectListItem {
                    Text = $"{tsuc.txt_nom_suc} ({tsuc.cod_suc})",
                    Value = tsuc.cod_suc.ToString()
                })
                .ToList();
        }

        /// <summary>
        /// Indica si existe el registro en la tabla [pv_header] con la
        /// llave primaria indicada por el Vista Modelo. Este método
        /// busca solamente el primer endoso ([nro_endoso] == 0).
        /// </summary>
        /// <param name="model">El Vista Modelo con las llaves primarias del registro a buscar.</param>
        /// <returns>True si el registro existe con esas llaves primarias. De lo contrario, regresa False.</returns>
        public bool ExistePolizaCoaseguro(ConsultarPolizaViewModel model)
        {
            return db.pv_header
                .Any(header =>
                    header.cod_suc == model.CodSucursal
                    && header.cod_ramo == model.CodRamo
                    && header.nro_pol == model.NroPoliza
                    && header.aaaa_endoso == model.Sufijo
                    && header.nro_endoso == 0M);
        }

        /// <summary>
        /// Regresa el [id_pv] de la póliza con los parámetros indicados. Este método
        /// busca solamente el primer endoso ([nro_endoso] == 0).
        /// </summary>
        /// <param name="model">El Vista Modelo con las llaves primarias del registro a buscar.</param>
        /// <returns>El [id_pv] de la póliza indicada.</returns>
        public int ObtenerIdPv(ConsultarPolizaViewModel model)
        {
            return db
                .pv_header
                .FirstOrDefault(header =>
                    header.cod_suc == model.CodSucursal
                    && header.cod_ramo == model.CodRamo
                    && header.nro_pol == model.NroPoliza
                    && header.aaaa_endoso == model.Sufijo
                    && header.nro_endoso == 0M)
                .id_pv;
        }

        /// <summary>
        /// Regresa el valor del campo [cod_operacion] de la póliza indicada.
        /// </summary>
        /// <param name="idPv">El Id de la póliza a consultar.</param>
        /// <returns>El código de la tabla [ttipo_mov] a la que pertenece la póliza.</returns>
        public decimal ObtenerTipoMovimiento(int idPv)
        {
            return db.pv_header
                .FirstOrDefault(header => header.id_pv == idPv)
                .cod_operacion;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue) {
                if (disposing) {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}