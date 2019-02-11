namespace WSEmision.Models.DAL.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    /// <summary>
    /// Conexi�n a la base de datos, especificada por una cadena de conexi�n
    /// en la configuraci�n de la aplicaci�n.
    /// </summary>
    public partial class EmisionContext : DbContext
    {
        public virtual DbSet<pv_header> pv_header { get; set; }
        public virtual DbSet<tramo> tramo { get; set; }
        public virtual DbSet<tsuc> tsuc { get; set; }

        /// <summary>
        /// Genera una nueva conexi�n a la base de datos en el
        /// entorno indicado.
        /// </summary>
        /// <param name="entorno">El nombre de la base de datos a la cual se conectar�. El
        /// nombre debe ser de una cadena de conexi�n especificada en web.config; por ejemplo:
        /// "name=UAT" o "name=Produccion"</param>
        public EmisionContext(string entorno) : base(entorno)
        {
            Database.Log = log => System.Diagnostics.Debug.WriteLine(log);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
