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
using GestEtude.Page;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace GestEtude.Report
{
    /// <summary>
    /// Logique d'interaction pour _1.xaml
    /// </summary>
    public partial class _1 : Window
    {
        public string id { get; set; }

        public _1(string codes)
        {
            InitializeComponent();
            id = codes;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Model1 db = new Model1();
           
                var res = (from et in db.Etudiant
                           select new
                            { 
                                et.Nom,
                                et.Prenom,
                                et.Mail,
                                et.Tel
                            }).Where(x => x.Nom == id).FirstOrDefault();

           if(res != null)
            {
                FicheEtu report = new FicheEtu();

                report.Load(@"FicheEtu.rpt");

                report.SetParameterValue("nom", res.Nom);
                report.SetParameterValue("prenom", res.Prenom);
                report.SetParameterValue("mail", res.Mail);
                report.SetParameterValue("tel", res.Tel);

                viewverEtu.ViewerCore.ReportSource = report;
                viewverEtu.ShowRefreshButton = true;
            }
            else
            {
                MessageBox.Show("Aucun etudiant de ce nom trouvé", "Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
            }


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
