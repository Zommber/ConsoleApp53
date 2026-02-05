using System;
using System.Globalization;

namespace ConvertidorMonedas
{
    // Guarda nombre, símbolo y tasa (pesos por dólar).
    public class Moneda
    {
        private string nombre;
        private string simbolo;
        private double tasaCambio;

        public Moneda(string nombre, string simbolo, double tasaCambio)
        {
            this.nombre = nombre;
            this.simbolo = simbolo;
            this.tasaCambio = tasaCambio;
        }

        public string GetNombre() => nombre;
        public string GetSimbolo() => simbolo;
        public double GetTasaCambio() => tasaCambio;

        // Cambia la tasa si es válida.
        public void SetTasaCambio(double nuevaTasa)
        {
            if (nuevaTasa > 0) tasaCambio = nuevaTasa;
            else Console.WriteLine("La tasa debe ser mayor que 0.");
        }

        public void MostrarInfo()
        {
            Console.WriteLine($"Moneda: {nombre}");
            Console.WriteLine($"Símbolo: {simbolo}");
            Console.WriteLine($"Tasa: {tasaCambio:F2}");
        }
    }