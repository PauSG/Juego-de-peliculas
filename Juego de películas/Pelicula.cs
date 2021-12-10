using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Juego_de_películas
{
    class Pelicula : ObservableObject
    {
        private string titulo;
        public string Titulo
        {
            get { return titulo; }
            set { SetProperty(ref titulo, value); }
        }

        private string pista;
        public string Pista
        {
            get { return pista; }
            set { SetProperty(ref pista, value); }
        }

        private string cartel;
        public string Cartel
        {
            get { return cartel; }
            set { SetProperty(ref cartel, value); }
        }

        private string nivel;
        public string Nivel
        {
            get { return nivel; }
            set { SetProperty(ref nivel, value); }
        }

        private string genero;
        public string Genero
        {
            get { return genero; }
            set { SetProperty(ref genero, value); }
        }

        public Pelicula(string titulo, string pista, string cartel, string nivel, string genero)
        {
            this.titulo = titulo;
            this.pista = pista;
            this.cartel = cartel;
            this.nivel = nivel;
            this.genero = genero;
        }
    }
}