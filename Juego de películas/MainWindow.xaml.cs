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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Juego_de_películas
{
    public partial class MainWindow : Window
    {
        MainWindowVM vm = new MainWindowVM();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void CargarJsonButton_Click(object sender, RoutedEventArgs e)
        {
            vm.CargarJson();
        }

        private void GuardarJsonButton_Click(object sender, RoutedEventArgs e)
        {
            vm.GuardarJson();
        }

        private void EditarPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            vm.EditarPelicula();
        }

        private void ConfirmarEdicionButton_Click(object sender, RoutedEventArgs e)
        {
            vm.ConfirmarEdicion();
        }

        private void EliminarPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            vm.EliminarPelicula();
        }

        private void AñadirPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            vm.AñadirPelicula();
        }

        private void NuevaPartidaButton_Click(object sender, RoutedEventArgs e)
        {
            vm.NuevaPartida();
        }

        private void FinalizarPartidaButton_Click(object sender, RoutedEventArgs e)
        {
            vm.FinalizarPartida();
            PistaCheckBox.IsChecked = false;
        }

        private void ValidarButton_Click(object sender, RoutedEventArgs e)
        {
            vm.Validar();
        }
    }
}
