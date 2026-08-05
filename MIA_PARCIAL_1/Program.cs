using System;
using System.IO;

namespace Parcial_I_Alejandro_Mogollon
{
    class Program
    {
        static void Main(string[] args)
        {
            //Entradas
            Console.Write("Ingrese su nombre completo: ");
            string nombreCompleto = Console.ReadLine()?.Trim() ?? string.Empty;

            string nombreFormateado = nombreCompleto.Replace(' ', '_');

            string rutaCarpeta = @"C:\MIA_Parcial1";
            string rutaArchivoTexto = Path.Combine(rutaCarpeta, $"{nombreFormateado}.txt");

            if (!File.Exists(rutaArchivoTexto))
            {
                Console.WriteLine();
                Console.WriteLine($"No se encontró el archivo en la ruta: {rutaArchivoTexto}");
                Console.WriteLine($"Asegúrese de crear primero la carpeta {rutaCarpeta} y el archivo correspondientes.");
                Console.WriteLine("Presione cualquier tecla para salir.");
                Console.ReadKey();
                return;
            }
            //Proceso
            try
            {
                string textoCompleto = File.ReadAllText(rutaArchivoTexto);

                int numeroLineas = 0;
                int numeroPalabras = 0;
                int numeroCaracteres = 0;
                bool enPalabra = false;

                if (textoCompleto.Length > 0)
                {
                    numeroLineas = 1;
                }

                for (int i = 0; i < textoCompleto.Length; i++)
                {
                    char c = textoCompleto[i];

                    numeroCaracteres++;

                    if (c == '\n')
                    {
                        numeroLineas++;
                    }

                    if (char.IsWhiteSpace(c))
                    {
                        enPalabra = false;
                    }
                    else if (!enPalabra)
                    {
                        enPalabra = true;
                        numeroPalabras++;
                    }
                }
                //Salida
                Console.WriteLine();
                Console.WriteLine($"Número de Líneas:     {numeroLineas}");
                Console.WriteLine($"Número de Palabras:   {numeroPalabras}");
                Console.WriteLine($"Número de Caracteres: {numeroCaracteres}");

                string directorioDestino = Path.GetDirectoryName(rutaArchivoTexto) ?? rutaCarpeta;
                string rutaCsv = Path.Combine(directorioDestino, $"resultados_{nombreFormateado}.csv");

                string contenidoCsv = $"Estudiante,Lineas,Palabras,Caracteres{Environment.NewLine}" +
                                     $"{nombreFormateado},{numeroLineas},{numeroPalabras},{numeroCaracteres}";

                File.WriteAllText(rutaCsv, contenidoCsv);

                Console.WriteLine();
                Console.WriteLine($"Archivo CSV generado en: {rutaCsv}");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Ocurrió un problema al procesar el archivo: {ex.Message}");
            }
        }
    }
}