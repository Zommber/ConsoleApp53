using System;
using System.Collections.Generic;

namespace ConvertidorMonedas
{
    // Clase simple para guardar líneas con las conversiones realizadas.
    public class HistorialConversiones
    {
        private readonly List<string> registros = new();


        public void AgregarConversion(DateTime fecha, string desde, string hasta, double cantidadDesde, double cantidadHasta, double tasa)
        {
            string linea = $"{fecha:yyyy-MM-dd HH:mm} - {cantidadDesde:F2} {desde} => {cantidadHasta:F2} {hasta} (Tasa: {tasa:F4})";
            registros.Add(linea);
        }

        // Muestra el historial por consola.
        public void Mostrar()
        {
            Console.WriteLine();
            if (registros.Count == 0)
            {
                Console.WriteLine("Historial vacío.");
                return;
            }

            Console.WriteLine("Historial de conversiones:");
            foreach (var r in registros)
            {
                Console.WriteLine(r);
            }
        }

        // Limpia el historial.
        public void Limpiar()
        {
            registros.Clear();
            Console.WriteLine("Historial borrado.");
        }
    }
}