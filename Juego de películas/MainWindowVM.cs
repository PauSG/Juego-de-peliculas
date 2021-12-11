using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Juego_de_películas
{
    class MainWindowVM : INotifyPropertyChanged
    {
        bool informarSobreModificaciones = false;
        public MainWindowVM()
        {
            tipoNivel = new List<string>() { "Fácil", "Media", "Difícil" };
            tipoGenero = new List<string>() { "Comedia", "Drama", "Acción", "Terror", "Ciencia-ficción" };
            editar = false;
        }

        private List<String> tipoNivel;

        public List<String> TipoNivel
        {
            get { return tipoNivel; }
            set
            {
                tipoNivel = value;
                this.NotifyPropertyChanged("TipoNivel");
            }
        }

        private List<string> tipoGenero;

        public List<string> TipoGenero
        {
            get { return tipoGenero; }
            set
            {
                tipoGenero = value;
                this.NotifyPropertyChanged("TipoGenero");
            }
        }


        private ObservableCollection<Pelicula> listaPeliculas;

        public ObservableCollection<Pelicula> ListaPeliculas
        {
            get { return listaPeliculas; }
            set
            {
                listaPeliculas = value;
                this.NotifyPropertyChanged("ListaPeliculas");
            }
        }


        private Pelicula peliculaActual;

        public Pelicula PeliculaActual
        {
            get { return peliculaActual; }
            set
            {
                peliculaActual = value;
                this.NotifyPropertyChanged("PeliculaActual");
            }
        }
        private bool editar;

        public bool Editar
        {
            get { return editar; }
            set
            {
                editar = value;
                this.NotifyPropertyChanged("Editar");
            }
        }

        public void NuevaPartida()
        {
            Random rnd = new Random();
            int indiceAleatorio = rnd.Next(0, ListaPeliculas.Count);
            PeliculaActual = ListaPeliculas[indiceAleatorio];
        }
        public void FinalizarPartida()
        {
            MessageBox.Show("Partida Finalizada\nLa pelicula era " + PeliculaActual.Titulo);
        }
        public void Validar()
        {

        }
        public void CargarJson()
        {
            try
            {
                string rutaJSON = ServicioOpenDialog.OpenJson();
                if (rutaJSON != null)
                {
                    string peliculasJson = File.ReadAllText(rutaJSON);
                    ListaPeliculas = JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(peliculasJson);
                }
            }
            catch (JsonReaderException)
            {
                MessageBox.Show("El archivo selecionado no es un JSON", "Extensión erronea", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void GuardarJson()
        {
            string rutaJSON = ServicioOpenDialog.OpenJson();
            if (rutaJSON != null && ListaPeliculas != null)
            {
                string peliculasJson = JsonConvert.SerializeObject(ListaPeliculas);
                File.WriteAllText(rutaJSON, peliculasJson);
            }
        }
        public void EditarPelicula()
        {
            if (PeliculaActual != null)
            {
                Editar = true;
            }
        }
        public void ConfirmarEdicion()
        {
            Editar = false;
            if (!informarSobreModificaciones)
            {
                MessageBox.Show("Para registrar y guardar cualquier modificacion (Editar, añadir o eliminar película) antes de cerrar la aplicación darle al boton de \"Guardar en JSON\"", "Información Sobre las modificaciones", MessageBoxButton.OK, MessageBoxImage.Information);
                informarSobreModificaciones = true;
            }
        }
        public void AñadirPelicula()
        {

            if (!informarSobreModificaciones)
            {
                MessageBox.Show("Para registrar y guardar cualquier modificacion (Editar, añadir o eliminar película) antes de cerrar la aplicación darle al boton de \"Guardar en JSON\"", "Información Sobre las modificaciones", MessageBoxButton.OK, MessageBoxImage.Information); informarSobreModificaciones = true;
            }
        }
        public void EliminarPelicula()
        {
            ListaPeliculas.Remove(PeliculaActual);

            if (!informarSobreModificaciones)
            {
                MessageBox.Show("Para registrar y guardar cualquier modificacion (Editar, añadir o eliminar película) antes de cerrar la aplicación darle al boton de \"Guardar en JSON\"", "Información Sobre las modificaciones", MessageBoxButton.OK, MessageBoxImage.Information); informarSobreModificaciones = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
