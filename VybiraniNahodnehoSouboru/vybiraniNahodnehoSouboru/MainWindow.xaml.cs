using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vybiraniNahodnehoSouboru
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        public string Cesta { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Cesta = Directory.GetCurrentDirectory();
        }
        string[] polozky;
        private void BTVyber_Click(object sender, RoutedEventArgs e)
        {
            if (Cesta != TxBxNovaCesta.Text) Cesta = TxBxNovaCesta.Text;
            try
            {
                if ((bool)RBVyberSlozku.IsChecked)
                {
                    if ((bool)ChBxPodslozky.IsChecked) polozky = Directory.GetDirectories(Cesta, "*", SearchOption.AllDirectories);
                    else polozky = Directory.GetDirectories(Cesta);

                    int nahoda = rnd.Next(0, polozky.Length);

                    string[] nazvySlozek = polozky[nahoda].Split(System.IO.Path.DirectorySeparatorChar);
                    TxBxCesta.Text = polozky[nahoda];
                    TxBxNazevPredmetu.Text = nazvySlozek[nazvySlozek.Length - 1];
                }
                else
                {
                    if ((bool)ChBxPodslozky.IsChecked) polozky = Directory.GetFiles(Cesta, "*.*", SearchOption.AllDirectories);
                    else polozky = Directory.GetFiles(Cesta, "*.*", SearchOption.TopDirectoryOnly);

                    int nahoda = rnd.Next(0, polozky.Length);
                    string[] nazvySouboru = polozky[nahoda].Split(System.IO.Path.DirectorySeparatorChar);

                    TxBxCesta.Text = polozky[nahoda];
                    TxBxNazevPredmetu.Text = nazvySouboru[nazvySouboru.Length - 1];
                }
            }
            catch
            {
                TxBxNovaCesta.Text = Directory.GetCurrentDirectory();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxBxNovaCesta.Text = Cesta;
        }

        private void BTTatoSlozka_Click(object sender, RoutedEventArgs e)
        {
            TxBxNovaCesta.Text = Directory.GetCurrentDirectory();
        }

        private void BTStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process process = Process.Start(TxBxCesta.Text);
            }
            catch { TxBxCesta.Text = Directory.GetCurrentDirectory(); }
        }
    }
}
