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
using System.Globalization;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Juego_de_películas
{
    class MainWindowVM : ObservableObject
    {
        List<Pelicula> listaJuego;
        bool informarSobreModificaciones = false;
        bool sePuedeAñadirPelicula = false;
        public MainWindowVM()
        {
            tipoNivel = new List<string>() { "Fácil", "Media", "Difícil" };
            tipoGenero = new List<string>() { "Comedia", "Drama", "Acción", "Terror", "Ciencia-ficción" };
            listaPeliculas = new ObservableCollection<Pelicula>();
            editar = false;
            Puntuacion = 0;
            JuegoInactivo = true;
            JsonCargado = false;
        }
        private bool jsonCargado;

        public bool JsonCargado
        {
            get { return jsonCargado; }
            set { SetProperty(ref jsonCargado, value); }
        }

        private string tituloTextBox;
        public string TituloTextBox
        {
            get { return tituloTextBox; }
            set { SetProperty(ref tituloTextBox, value); }
        }

        private int puntuacion;
        public int Puntuacion
        {
            get { return puntuacion; }
            set { SetProperty(ref puntuacion, value); }
        }

        private List<String> tipoNivel;

        public List<String> TipoNivel
        {
            get { return tipoNivel; }
            set { SetProperty(ref tipoNivel, value); }
        }

        private List<string> tipoGenero;

        public List<string> TipoGenero
        {
            get { return tipoGenero; }
            set { SetProperty(ref tipoGenero, value); }
        }


        private ObservableCollection<Pelicula> listaPeliculas;

        public ObservableCollection<Pelicula> ListaPeliculas
        {
            get { return listaPeliculas; }
            set { SetProperty(ref listaPeliculas, value); }
        }


        private Pelicula peliculaActual;

        public Pelicula PeliculaActual
        {
            get { return peliculaActual; }
            set { SetProperty(ref peliculaActual, value); }
        }
        private bool editar;

        public bool Editar
        {
            get { return editar; }
            set { SetProperty(ref editar, value); }
        }



        private bool juegoInactivo;

        public bool JuegoInactivo
        {
            get { return juegoInactivo; }
            set { SetProperty(ref juegoInactivo, value); }
        }

        private bool juegoActivo;
            
        public bool JuegoActivo
        {
            get { return juegoActivo; }
            set { SetProperty(ref juegoActivo, value); }
        }

        private bool pistaActivada;

        public bool PistaActivada
        {
            get { return pistaActivada; }
            set { SetProperty(ref pistaActivada, value); }
        }


        public void NuevaPartida()
        {
            if (ListaPeliculas.Count >= 5)
            {
                listaJuego = new List<Pelicula>();
                JuegoInactivo = false;
                JuegoActivo = true;
                Random rnd = new Random();
                Pelicula nuevaPelicula;
                try
                {
                    for (int i = 0; i < 5; i++)
                    {
                        do
                        {
                            int indiceAleatorio = rnd.Next(0, ListaPeliculas.Count);
                            nuevaPelicula = ListaPeliculas[indiceAleatorio];
                        } while (listaJuego.Contains(nuevaPelicula));
                        listaJuego.Add(nuevaPelicula);
                    }
                    PeliculaActual = listaJuego[0];
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("No hay cargada ninguna pelicula en la gestion");
                }
            }
            else MessageBox.Show("Tiene que haber almenos 5 Peliculas en la lista para poder empezar una partida");

        }
        public void FinalizarPartida()
        {
            listaJuego.RemoveRange(0, listaJuego.Count);
            MessageBox.Show("Tu puntuacion final ha sido de " + Puntuacion, "Partida Finalizada");
            Puntuacion = 0;
            JuegoInactivo = true;
            JuegoActivo = false;
            PeliculaActual = null;
            TituloTextBox = "";
        }
        public void Validar()
        {
            if (TituloTextBox != null)
            {
                if (QuitarAcentos(TituloTextBox).ToLower().Equals(QuitarAcentos(PeliculaActual.Titulo.ToLower())))
                {
                    if (PistaActivada == false)
                    {
                        switch (PeliculaActual.Nivel)
                        {
                            case "Fácil":
                                Puntuacion += 100;
                                break;
                            case "Media":
                                Puntuacion += 300;
                                break;
                            case "Difícil":
                                Puntuacion += 500;
                                break;
                        }
                    }
                    else
                    {
                        switch (PeliculaActual.Nivel)
                        {
                            case "Fácil":
                                Puntuacion += 100/2;
                                break;
                            case "Media":
                                Puntuacion += 300/2;
                                break;
                            case "Difícil":
                                Puntuacion += 500/2;
                                break;
                        }
                    }
                    SiguientePelicula();
                    
                    PistaActivada = false;
                }
                else
                {
                    MessageBox.Show("Te has equivocado de película, intentalo otra vez");
                }
                TituloTextBox = "";
            }
        }
        public void SiguientePelicula()
        {
            listaJuego.Remove(PeliculaActual);
            if (listaJuego.Count > 0) PeliculaActual = listaJuego[0];
            else
            {
                FinalizarPartida();
            }
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
                    JsonCargado = true;
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
            PeliculaActual = null;
            PeliculaActual = new Pelicula("", "", "", "", "");
            Editar = true;
        }
        public void ConfirmarAñadir()
        {
            if (PeliculaActual.Titulo != "" && PeliculaActual.Cartel != "" && PeliculaActual.Nivel != "" && PeliculaActual.Pista != "" && PeliculaActual.Genero != "")
            {
                if (!ListaPeliculas.Contains(PeliculaActual))
                {
                    ListaPeliculas.Add(PeliculaActual);
                }
                else
                {
                    MessageBox.Show("La película Ya existia y no se a añadido nuevamente", "Coincidencia Encontrada", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                Editar = false;
            }
            else
            {
                MessageBox.Show("Tienes que rellenar todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!informarSobreModificaciones)
            {
                MessageBox.Show("Para registrar y guardar cualquier modificacion (Editar, añadir o eliminar película) antes de cerrar la aplicación darle al boton de \"Guardar en JSON\"", "Información Sobre las modificaciones", MessageBoxButton.OK, MessageBoxImage.Information);
                informarSobreModificaciones = true;
            }
            PeliculaActual = null;
        }
        public void EliminarPelicula()
        {
            ListaPeliculas.Remove(PeliculaActual);

            if (!informarSobreModificaciones)
            {
                MessageBox.Show("Para registrar y guardar cualquier modificacion (Editar, añadir o eliminar película) antes de cerrar la aplicación darle al boton de \"Guardar en JSON\"", "Información Sobre las modificaciones", MessageBoxButton.OK, MessageBoxImage.Information);
                informarSobreModificaciones = true;
            }
        }

        public static string QuitarAcentos(string inputString)
        {
            string normalizedString = inputString.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(normalizedString[i]);
                }
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
