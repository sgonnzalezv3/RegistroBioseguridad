namespace RegistroBioseguridad
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visitante")]
    public partial class Visitante
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(25)]
        public string Identificacion { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoIngreso { get; set; }

        [Column(Order = 2)]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Column(Order = 4)]
        [StringLength(50)]
        public string Organizacion { get; set; }

        [Column(Order = 5)]
        [StringLength(15)]
        public string FechaIngreso { get; set; }
    }
}
