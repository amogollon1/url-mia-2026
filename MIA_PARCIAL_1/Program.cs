using System;
using System.IO;
using System.Linq;

namespace Parcial_I_Alejandro_Mogollon
{
    class Program
    {
        static void Main(string[] args)
        {
            // Entradas
            Console.Write("Ingrese su nombre completo: ");
            string nombreCompleto = Console.ReadLine()?.Trim() ?? string.Empty;

            string nombreFormateado = string.Join("_", nombreCompleto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            string rutaCarpeta = @"C:\MIA_Parcial1";
            string rutaArchivoTexto = Path.Combine(rutaCarpeta, $"{nombreFormateado}.txt");

            if (!File.Exists(rutaArchivoTexto))
            {
                Console.WriteLine();
                Console.WriteLine($"[ERROR] No se encontró el archivo en la ruta: {rutaArchivoTexto}");
                Console.WriteLine($"Asegúrese de crear primero la carpeta {rutaCarpeta} y el archivo correspondientes.");
                Console.WriteLine("Presione cualquier tecla para salir.");
                Console.ReadKey();
                return;
            }
            //Proceso
            try
            {
                string[] lineas = File.ReadAllLines(rutaArchivoTexto);
                string textoCompleto = File.ReadAllText(rutaArchivoTexto);

                int numeroLineas = lineas.Length;
                int numeroCaracteres = textoCompleto.Length;

                char[] separadores = new char[] { ' ', '\r', '\n', '\t' };
                int numeroPalabras = textoCompleto.Split(separadores, StringSplitOptions.RemoveEmptyEntries).Length;
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
                Console.WriteLine($"[ERROR] Ocurrió un problema al procesar el archivo: {ex.Message}");
            }
        }
    }
}