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
using System.Windows.Threading;

namespace GestEtude
{
    /// <summary>
    /// Logique d'interaction pour Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        DispatcherTimer dispatcher = new DispatcherTimer();
        public Splash()
        {
            InitializeComponent();
            dispatcher.Tick += Dispatcher_Tick;
            dispatcher.Interval = new TimeSpan(0, 0, 7);
            dispatcher.Start();
        }

        private void Dispatcher_Tick(object sender, EventArgs e)
        {
            new MainWindow().Show();
            dispatcher.Stop();
            this.Close();
        }
    }
}
