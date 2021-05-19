namespace GestEtude.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ensselect")]
    public partial class Ensselect
    {
        [Key]
        [Column(Order = 0)]
        public int NoEnseignant { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Tel { get; set; }

        public string Mail { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Indice { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodeDep { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoCours { get; set; }

        [StringLength(10)]
        public string Role { get; set; }

        [StringLength(4000)]
        public string DatePrise { get; set; }
    }
}
