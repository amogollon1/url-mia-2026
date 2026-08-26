using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

string rutaCSV = "estudiantes.csv";
string rutaJSON = "estudiantes.json"; // es el nombre que va a tener el archivo JSON que se va a generar

if (!File.Exists(rutaCSV))
{
    Console.WriteLine($"ERROR: El archivo {rutaCSV} no existe.");
    return;
}
    
string[] lineas = File.ReadAllLines(rutaCSV); //lee todas las líneas del archivo CSV y las almacena en un arreglo de strings

List<Estudiante> listaEstudiantes = new List<Estudiante>();

for(int i = 1; i < lineas.Length; i++) //comienza desde 1 para omitir la primera línea que contiene los encabezados
{
    string linea = lineas[i];

    if(string.IsNullOrWhiteSpace(linea)) continue;

    string[] datos = linea.Split(','); //separa cada linea en un arreglo de strings usando la coma como delimitador

    Estudiante estudiante = new Estudiante
    {
        Id = int.Parse(datos[0].Trim()),
        Nombre = datos[1].Trim(),
        Carrera = datos[2].Trim()
    };
    listaEstudiantes.Add(estudiante);
}

Console.WriteLine("Lista de estudiantes");
foreach(Estudiante estudiante in listaEstudiantes)
{
    Console.WriteLine($"ID: {estudiante.Id}, Nombre: {estudiante.Nombre}, Carrera: {estudiante.Carrera}");
}

string jsonString = JsonSerializer.Serialize(listaEstudiantes, new JsonSerializerOptions { WriteIndented = true });
File.WriteAllText(rutaJSON, jsonString);
Console.WriteLine("Archivo JSON generado correctamente.");