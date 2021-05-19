
using GestEtude.Models;
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
    /// Logique d'interaction pour Enseignant.xaml
    /// </summary>
    public partial class Enseignant : UserControl
    {

        private ICollection<GestEtude.Models.Cours> cours { get; set; }
        private ICollection<GestEtude.Models.Departement> departements { get; set; }

        public Enseignant()
        {
            InitializeComponent();
            LierCombobox();
            btnAjouter.Click += BtnAjouter_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            LoadData();
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();
            int.TryParse(txtID.Text, out int code);
            var delete = from c in db.Enseignant
                         where c.NoEnseignant == code
                         select c;

            GestEtude.Models.Enseignant ens = delete.SingleOrDefault();

            if (ens != null)
            {
                db.Enseignant.Remove(ens);
                db.SaveChanges();
            }

            LoadData();
            Effacer();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(txtID.Text, out int code);
            Int32.TryParse(txtIndice.Text, out int indice);
            Model1 db = new Model1();
            DataContext = db;
            string nom = txtNom.Text.ToString();
            var update = from c in db.Enseignant
                         where c.NoEnseignant == code
                         select c;
            GestEtude.Models.Enseignant ens = update.SingleOrDefault();

            if (txtNom.Text != "" && txtPrenom.Text != "" && txtPhone.Text != "" && txtMail.Text != "" && txtDate.SelectedDate != null && txtIndice.Text != "" && ComboCours.Text != "" && ComboRole.Text != "")
            {
                Int32.TryParse(ComboCours.SelectedValue.ToString(), out int CodeCours);
                Int32.TryParse(ComboDep.SelectedValue.ToString(), out int CodeDep);
                if (ens != null)
                {
                    ens.Nom = txtNom.Text;
                    ens.Prenom = txtPrenom.Text;
                    ens.Tel = txtPhone.Text;
                    ens.Mail = txtMail.Text;
                    ens.DatePrise = txtDate.SelectedDate;
                    ens.Indice = indice;
                    ens.CodeDep = CodeDep;
                    ens.NoCours = CodeCours;
                    MessageBox.Show("Mise à jour effectué");
                    db.SaveChanges();
                }

                LoadData();
                Effacer();
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();



            if (txtNom.Text != "" && txtPrenom.Text != "" && txtPhone.Text != "" && txtMail.Text != "" && txtDate.SelectedDate != null && txtIndice.Text != "" && ComboCours.Text != "" && ComboRole.Text != "" && ComboDep.Text != "")
            {
                Int32.TryParse(ComboCours.SelectedValue.ToString(), out int CodeCours);
                Int32.TryParse(ComboDep.SelectedValue.ToString(), out int CodeDep);

                GestEtude.Models.Enseignant enseignant = new GestEtude.Models.Enseignant()
                {
                    

                    Nom = txtNom.Text,
                    Prenom = txtPrenom.Text,
                    Tel = txtPhone.Text,
                    Mail = txtMail.Text,
                    Indice = Int32.Parse(txtIndice.Text),
                    Role = ComboRole.SelectedValue.ToString(),
                    DatePrise = DateTime.Parse(txtDate.Text),
                    NoCours = CodeCours,
                    CodeDep = CodeDep

                };

                db.Enseignant.Add(enseignant);
                db.SaveChanges();
                LoadData();
                Effacer();
            }
        }


        private void LoadData()
        {

            Model1 db = new Model1();
            DataContext = db;

            var tousEns = (
                from e in db.Enseignant
                join c in db.Cours
                on e.NoCours equals c.NoCours
                join d in db.Departement
                on e.CodeDep equals d.CodeDep
                select new
                {
                    e.NoEnseignant,
                    e.Mail,
                    e.Indice,
                    e.Nom,
                    e.Prenom,
                    e.Role,
                    e.Tel,
                    e.DatePrise,
                    c.Libelle,
                    d.NomDep
                }
                ).ToList();


            EnsData.ItemsSource = tousEns;
        }

        public void LierCombobox()
        {
            Model1 db = new Model1();

            var items = db.Cours.ToList();

            cours = items;
            ComboCours.ItemsSource = cours;

            var itemDep = db.Departement.ToList();
            departements = itemDep;
            ComboDep.ItemsSource = departements;


        }

        private void ComboRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void ComboCours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Effacer()
        {
            txtID.Clear();
            txtNom.Clear();
            txtDate.Text = "";
            txtPrenom.Clear();
            txtMail.Clear();
            txtIndice.Clear();
            txtPhone.Clear();

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void BtnImprimer_Click(object sender, RoutedEventArgs e)
        {
            if(txtNom.Text != "")
            {
                new GestEtude.Report._1_1(txtNom.Text).ShowDialog();
            }
        }
    }
}
