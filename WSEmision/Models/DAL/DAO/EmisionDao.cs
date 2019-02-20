using System;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;

using WSEmision.Models.DAL.DTO;
using WSEmision.Models.DAL.Entities;

namespace WSEmision.Models.DAL.DAO
{
    /// <summary>
    /// Clase base con las operaciones comunes que realizan los DAO
    /// de esta aplicación.
    /// </summary>
    public abstract class EmisionDao : IDisposable
    {
        /// <summary>
        /// El nombre del entorno de base de datos a utilizar.
        /// </summary>
        protected string entorno;

        /// <summary>
        /// La conexión hacia la base de datos.
        /// </summary>
        protected EmisionContext db;

        /// <summary>
        /// Genera una nueva conexión a la base de datos, tomando
        /// el entorno del archivo [web.config].
        /// </summary>
        public EmisionDao()
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