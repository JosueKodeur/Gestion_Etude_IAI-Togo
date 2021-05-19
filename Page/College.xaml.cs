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

using GestEtude.Models;

namespace GestEtude.Page
{
    /// <summary>
    /// Logique d'interaction pour College.xaml
    /// </summary>
    public partial class College : UserControl
    {

        public College()
        {
            InitializeComponent();

            btnAjouter.Click += BtnAjouter_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            txtNom.Clear();
            txtSite.Clear();

            Model1 db = new Model1();

            LoadData();

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();
            int.TryParse(txtID.Text, out int code);
            var delete = from c in db.College
                         where c.CodeCollege == code
                         select c;

            GestEtude.Models.College college = delete.SingleOrDefault();

            if (college != null)
            {
                db.College.Remove(college);
                db.SaveChanges();
            }
            txtNom.Clear();
            txtSite.Clear();
            txtID.Clear();
            LoadData();

            
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();
            int.TryParse(txtID.Text, out int code);
            var update = from c in db.College
                         where c.CodeCollege == code
                         select c;

            GestEtude.Models.College college = update.SingleOrDefault();

            if(college != null)
            {
                college.NomCollege = this.txtNom.Text;
                college.site = this.txtSite.Text;
                db.SaveChanges();
            }

            txtNom.Clear();
            txtSite.Clear();

            LoadData();

            
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();

            

            if(txtNom.Text != "" && txtSite.Text != "")
            {
                GestEtude.Models.College college = new GestEtude.Models.College()
                {
                    NomCollege = txtNom.Text,
                    site = txtSite.Text
                };

                db.College.Add(college);
                db.SaveChanges();
                txtNom.Clear();
                txtSite.Clear();
                txtID.Clear();
                LoadData();
            }

            
        }

        private void LoadData()
        {

            Model1 db = new Model1();

            var tousColleges = (from c in db.College
                               select c).ToList();
      
            this.CollegeData.ItemsSource = tousColleges;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
