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
    /// Logique d'interaction pour Etudiant.xaml
    /// </summary>
    public partial class Etudiant : UserControl
    {

        public Etudiant()
        {
            InitializeComponent();
            Model1 db = new Model1();

            LoadData();

            BtnImprimer.Click += BtnImprimer_Click;
            btnAjouter.Click += BtnAjouter_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;


        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();
            int.TryParse(txtID.Text, out int code);
            var delete = from c in db.Etudiant
                         where c.NoEtudiant == code
                         select c;

            GestEtude.Models.Etudiant etu = delete.SingleOrDefault();

            if (etu != null)
            {
                db.Etudiant.Remove(etu);
                db.SaveChanges();
            }
            
            LoadData();
            Effacer();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();
            int.TryParse(txtID.Text, out int code);
            var update = from c in db.Etudiant
                         where c.NoEtudiant == code
                         select c;
            GestEtude.Models.Etudiant etu = update.SingleOrDefault();

            if (txtNom.Text != "" && txtPrenom.Text != "" && txtPhone.Text != "" && txtMail.Text != "" && txtDate.SelectedDate != null)
            {

                if (etu != null)
                {
                    etu.Nom = txtNom.Text;
                    etu.Prenom = txtPrenom.Text;
                    etu.Tel = txtPhone.Text;
                    etu.Mail = txtMail.Text;
                    etu.AnneeEntre = txtDate.SelectedDate;
                    db.SaveChanges();
                }

                LoadData();
                Effacer();
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();



            if (txtNom.Text != "" && txtPrenom.Text != "" && txtPhone.Text != "" && txtMail.Text != "" && txtDate.SelectedDate != null)
            {
                

                GestEtude.Models.Etudiant etudiant = new GestEtude.Models.Etudiant()
                {

                    Nom = txtNom.Text,
                    Prenom = txtPrenom.Text,
                    Tel = txtPhone.Text,
                    Mail = txtMail.Text,
                    AnneeEntre = DateTime.Parse(txtDate.Text),
                };

                db.Etudiant.Add(etudiant);
                db.SaveChanges();
                LoadData();
                Effacer();
            }
        }

        private void BtnImprimer_Click(object sender, RoutedEventArgs e)
        {
                

                if(txtNom.Text != "")
                {

                new GestEtude.Report._1(txtNom.Text).ShowDialog();

                }
        }


        private void LoadData()
        {

            Model1 db = new Model1();

            var tousEtu = (from d in db.Etudiantselect
                           select d).ToList();

            EtuData.ItemsSource = tousEtu;

        }


        

        private void txtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Effacer()
        {
            txtNom.Clear();
            txtID.Clear();
            txtDate.Text = "";
            txtPrenom.Clear();
            txtMail.Clear();
            txtPhone.Clear();
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Fill()
        {
            if (txtID.Text.Length > 0)
            {
                txtNom.Text = "";
            }
        }

        private void BtnImprimer_Copy_Click(object sender, RoutedEventArgs e)
        {
            new GestEtude.Report.EtuMat(txtNom.Text).ShowDialog();
        }

        private void BtnImprimer_Copy1_Click(object sender, RoutedEventArgs e)
        {



        }
    }
}
