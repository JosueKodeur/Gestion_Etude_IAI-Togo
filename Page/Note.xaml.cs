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
    /// Logique d'interaction pour Note.xaml
    /// </summary>
    public partial class Note : UserControl
    {
        private ICollection<GestEtude.Models.Cours> cours { get; set; }
        private ICollection<GestEtude.Models.Etudiant> etudiants { get; set; }
        private int updateIdEtu = 0;
        private int updateIdCours = 0;
        public Note()
        {
            InitializeComponent();
            LierCombobox();
            LoadData();
            btnAjouter.Click += BtnAjouter_Click;
            btnDelete.Click += BtnDelete_Click;
            btnUpdate.Click += BtnUpdate_Click;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();

            var update = from c in db.Note
                         where (c.NoEtudiant == updateIdEtu && c.NoCours == updateIdCours)
                         select c;

            GestEtude.Models.Note note = update.SingleOrDefault();

            if (note != null)
            {
                note.NoteControle = Int32.Parse(this.txtNom.Text);
                db.SaveChanges();
            }

            txtNom.Clear();

            LoadData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();

            var delete = from c in db.Note
                         where (c.NoEtudiant == updateIdEtu && c.NoCours == updateIdCours)
                         select c;

            GestEtude.Models.Note note = delete.SingleOrDefault();

            if (note != null)
            {
                db.Note.Remove(note);
                db.SaveChanges();
            }
            txtNom.Clear();
            LoadData();
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Model1 db = new Model1();



            if (txtNom.Text != "" && ComboCours.Text!= "" && ComboEtudiant.Text != "")
            {
                GestEtude.Models.Note note = new GestEtude.Models.Note()
                {
                    NoteControle = Int32.Parse(txtNom.Text),
                    NoEtudiant = Int32.Parse(ComboEtudiant.SelectedValue.ToString()),
                    NoCours = Int32.Parse(ComboCours.SelectedValue.ToString())
                };

                db.Note.Add(note);
                db.SaveChanges();
                txtNom.Clear();
                LoadData();
            }

        }

        private void LoadData()
        {

            Model1 db = new Model1();

            var touteNote = (from c in db.Note
                                select c).ToList();

           var note = (from n in db.Note
                       join e in db.Etudiant
                       on n.NoEtudiant equals e.NoEtudiant
                       join c in db.Cours
                       on n.NoCours equals c.NoCours
                       select new
                       {
                          e.Nom,
                          n.NoteControle,
                          c.Libelle
                       }).ToList();

            this.NoteData.ItemsSource = note;

        }

        public void LierCombobox()
        {
            Model1 db = new Model1();

            var items = db.Cours.ToList();

            cours = items;
            ComboCours.ItemsSource = cours;

            var itemEtu = db.Etudiant.ToList();
            etudiants = itemEtu;
            ComboEtudiant.ItemsSource = etudiants;


        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NoteData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.NoteData.SelectedIndex >= 0)
            {
                if (NoteData.SelectedItems.Count >= 0)
                {
                    if (this.NoteData.SelectedItems[0].GetType() == typeof(GestEtude.Models.Note))
                    {
                        GestEtude.Models.Note d = (GestEtude.Models.Note)this.NoteData.SelectedItems[0];

                        txtNom.Text = d.NoteControle.ToString();
                        updateIdEtu = d.NoEtudiant;
                        updateIdCours = d.NoCours;


                    }
                }
            }
        }
    }
}
