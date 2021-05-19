using GestEtude.Models;
using GestEtude.Report;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Dep.xaml
    /// </summary>
    public partial class Dep : UserControl
    {
        private ICollection<GestEtude.Models.College> colleges { get; set; }

        public Dep()
        {
            InitializeComponent();
            LoadData();
            LierCombobox();
            btnAjouter.Click += BtnAjouter_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnImpr.Click += BtnImpr_Click;

            
        }

        private void BtnImpr_Click(object sender, RoutedEventArgs e)
        {
            new Report.ListDep().ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();
            int.TryParse(txtID.Text, out int code);
            var delete = from c in db.Departement
                         where c.CodeDep == code
                         select c;

            GestEtude.Models.Departement dep = delete.SingleOrDefault();

            if (dep != null)
            {
                db.Departement.Remove(dep);
                db.SaveChanges();
            }
            txtNom.Clear();
            LoadData();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();
            int.TryParse(txtID.Text, out int code);
            var update = from c in db.Departement
                         where c.CodeDep == code
                         select c;
            GestEtude.Models.Departement dep = update.SingleOrDefault();

            if (txtNom.Text !="" && ComboCollege.SelectedItem != null)
            {
               
                Int32.TryParse(ComboCollege.SelectedValue.ToString(), out int Code);

                if (dep != null)
                {
                    dep.NomDep = txtNom.Text;
                    dep.CodeCollege = Code;
                    db.SaveChanges();
                }

                LoadData();
            }

        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();

            

            if(txtNom.Text != "" && ComboCollege.SelectedValue != null)
            {

                Int32.TryParse(ComboCollege.SelectedValue.ToString(), out int Code);
                Models.Departement departement = new Models.Departement()
                {
                    NomDep = txtNom.Text,
                    CodeCollege = Code
                };

                db.Departement.Add(departement);
                txtNom.Clear();
                db.SaveChanges();
                LoadData();
            }
            
        }

        public void LierCombobox()
        {
            Model1 db = new Model1();

            var items = db.College.ToList();

            colleges = items;
            ComboCollege.ItemsSource = colleges;

        }

        private void LoadData()
        {

            Model1 db = new Model1();

            var tousDep = (from d in db.Departement
                           join c in db.College
                           on d.CodeCollege equals c.CodeCollege

                           select new
                           {
                               d.CodeDep,
                               d.NomDep,
                               c.NomCollege
                           }
                         
                         ).ToList();

            DepData.ItemsSource = tousDep;

        }

        private void ComboCollege_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LierCombobox();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
