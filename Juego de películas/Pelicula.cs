using Microsoft.Toolkit.Mvvm.ComponentModel;

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
}

