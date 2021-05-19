namespace GestEtude.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Departement")]
    public partial class Departement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departement()
        {
            Enseignant = new HashSet<Enseignant>();
        }

        [Key]
        public int CodeDep { get; set; }

        public string NomDep { get; set; }

        public int CodeCollege { get; set; }

        public virtual College College { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enseignant> Enseignant { get; set; }
    }
}
