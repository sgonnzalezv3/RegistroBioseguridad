using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RegistroBioseguridad
{
    public partial class ContextBd : DbContext
    {
        public ContextBd()
            : base("name=ContextBd")
        {
        }

        public virtual DbSet<Visitante> Visitante { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
