using GestEtude.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestEtude.Report
{
    /// <summary>
    /// Logique d'interaction pour EtuMat.xaml
    /// </summary>
    public partial class EtuMat : Window
    {
        public string id;
        public EtuMat(string code)
        {
            InitializeComponent();
            id = code;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();

            var tousEns = (from d in db.Note 
                           join et in db.Etudiant
                           on d.NoEtudiant equals et.NoEtudiant
                           join m in db.Cours
                           on d.NoCours equals m.NoCours
                           where m.Libelle == id
                           select new
                           {
                               d.NoteControle,
                               m.Libelle,
                               et.Nom,
                               et.Prenom
                           }
                          

                           ).ToList();


            if (tousEns != null)
            {
                EtudMat fiche = new EtudMat();
                fiche.Load(@"EtudMat.rpt");

                fiche.SetDataSource(tousEns);
                viewverCourProf.ViewerCore.ReportSource = fiche;

                viewverCourProf.ShowRefreshButton = true;
            }
            else
            {
                MessageBox.Show("Aucun enseignant de ce nom trouvé", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);

            }
        }
    }
}
