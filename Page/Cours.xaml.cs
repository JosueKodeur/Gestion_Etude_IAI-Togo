
using Dapper;
using GestEtude.Models;
using GestEtude.Report;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestEtude.Page
{
    /// <summary>
    /// Logique d'interaction pour Cours.xaml
    /// </summary>
    public partial class Cours : UserControl
    {
      
        public Cours()
        {
            InitializeComponent();
            LoadData();
            btnAjouter.Click += BtnAjouter_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnImp_Click(object sender, RoutedEventArgs e)
        {
            if(txtMat.Text != "")
            {

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

                    string query = $"SELECT c.Libelle, e.Nom FROM Enseignant e JOIN Cours c On e.NoCours = c.NoCours WHERE c.Libelle = '{txtNom.Text}'";

                    List<GestEtude.Models.CoursEns> coursEns = db.Query<GestEtude.Models.CoursEns>(query, commandType: CommandType.Text).ToList();
              
                    new CourProf(coursEns, txtNom.Text).ShowDialog();
                }

                
            }

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();
            int.TryParse(txtID.Text, out int code);
            var delete = from c in db.Cours
                         where c.NoCours == code
                         select c;

            GestEtude.Models.Cours cours = delete.SingleOrDefault();

            if (cours != null)
            {
                db.Cours.Remove(cours);
                db.SaveChanges();
            }
            txtNom.Clear();
            txtID.Clear();
            LoadData();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();
            int.TryParse(txtID.Text, out int code);
            var update = from c in db.Cours
                         where c.NoCours == code
                         select c;

            GestEtude.Models.Cours cours = update.SingleOrDefault();

            if (cours != null)
            {
                if(txtNom.Text != "")
                {
                    cours.Libelle = txtNom.Text;
                    db.SaveChanges();
                }
            }
            LoadData();
            txtNom.Clear();
            txtID.Clear();
        }


        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();



            if (txtNom.Text != "")
            {
                GestEtude.Models.Cours cours = new GestEtude.Models.Cours()
                {
                    Libelle = txtNom.Text,
         
                };

                db.Cours.Add(cours);
                db.SaveChanges();
                txtNom.Clear();
                txtID.Clear();
                LoadData();
            }

        }

        private void LoadData()
        {

            Model1 db = new Model1();

            var tousCours = (from c in db.Cours
                                select c).ToList();

            this.CoursData.ItemsSource = tousCours;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
