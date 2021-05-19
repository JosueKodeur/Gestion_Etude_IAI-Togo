using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GestEtude.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<College> College { get; set; }
        public virtual DbSet<Cours> Cours { get; set; }
        public virtual DbSet<Departement> Departement { get; set; }
        public virtual DbSet<Enseignant> Enseignant { get; set; }
        public virtual DbSet<Etudiant> Etudiant { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Ensselect> Ensselect { get; set; }
        public virtual DbSet<Etudiantselect> Etudiantselect { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enseignant>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Ensselect>()
                .Property(e => e.Role)
                .IsUnicode(false);
        }
    }
}
