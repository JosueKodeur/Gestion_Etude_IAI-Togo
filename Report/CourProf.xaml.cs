using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Logique d'interaction pour CourProf.xaml
    /// </summary>
    public partial class CourProf : Window
    {
        List<GestEtude.Models.CoursEns> matiere { get; set; }
        public string libelle { get; set; }
        public CourProf(List<GestEtude.Models.CoursEns> mat, string lib)
        {
            InitializeComponent();
            matiere = mat;
            libelle = lib;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

            MatEns fiche = new MatEns();
            fiche.Load(@"MatEns.rpt");
            fiche.SetDataSource(matiere);
            fiche.SetParameterValue("matiere", libelle);
            viewverCourProf.ViewerCore.ReportSource = fiche;
            viewverCourProf.ShowRefreshButton = true;

        }

    }
}


