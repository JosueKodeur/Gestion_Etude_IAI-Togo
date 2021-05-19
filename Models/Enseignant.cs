namespace GestEtude.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enseignant")]
    public partial class Enseignant
    {
        [Key]
        public int NoEnseignant { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Tel { get; set; }

        public string Mail { get; set; }

        public DateTime? DatePrise { get; set; }

        public int Indice { get; set; }

        public int CodeDep { get; set; }

        public int NoCours { get; set; }

        [StringLength(10)]
        public string Role { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Departement Departement { get; set; }
    }
}
