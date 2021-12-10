using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Juego_de_películas
{
    class ServicioOpenDialog
    {
        public static string OpenJson()
        {
            string ruta=null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                ruta = openFileDialog.FileName;
            return ruta;
        }
    }
}
