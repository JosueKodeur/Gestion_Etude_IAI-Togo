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
    /// Logique d'interaction pour _1_1.xaml
    /// </summary>
    public partial class _1_1 : Window
    {
        public string id;
        public _1_1(string code)
        {
            InitializeComponent();
            id = code;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();

            var tousEns = (from d in db.Enseignant
                           where d.Nom == id
                           select d

                           ).FirstOrDefault();

            
            if(tousEns != null)
            {
                FicheEns fiche = new FicheEns();
                fiche.Load(@"FicheEns.rpt");


                fiche.SetParameterValue("nom", tousEns.Nom);
                fiche.SetParameterValue("prenom", tousEns.Prenom);
                fiche.SetParameterValue("mail", tousEns.Mail);
                fiche.SetParameterValue("tel", tousEns.Tel);
                viewver.ViewerCore.ReportSource = fiche;
                
                viewver.ShowRefreshButton = true;
            }
            else
            {
                MessageBox.Show("Aucun enseignant de ce nom trouvé", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
         
        }
    }
}
