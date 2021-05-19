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
    /// Logique d'interaction pour ListDep.xaml
    /// </summary>
    public partial class ListDep : Window
    {
        public ListDep()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            using (IDbConnection db2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
            {
                if (db2.State == ConnectionState.Closed)
                {
                    db2.Open();
                }

                string requete = "SELECT d.NomDep, c.NomCollege, e.Nom AS NomDirecteur, e.Role" +
                             " FROM Departement d JOIN College c on d.CodeCollege = c.CodeCollege" +
                             " JOIN Enseignant e on e.CodeDep = d.CodeDep" +
                             " Group By d.NomDep, c.NomCollege, e.Nom, e.Role";


                List<GestEtude.Models.ListDep> listDeps = db2.Query<GestEtude.Models.ListDep>(requete, commandType: CommandType.Text).ToList();

                if (listDeps !=null)
                {
                    ListDepart listDepart = new ListDepart();
                    listDepart.Load(@"ListDepart.rpt");
                    listDepart.SetDataSource(listDeps);
                    viewverListDep.ViewerCore.ReportSource = listDepart;
                    viewverListDep.ShowRefreshButton = true;
                }
                else
                {
                    MessageBox.Show("Aucune reponse");
                }
            }
        }
    }
}
