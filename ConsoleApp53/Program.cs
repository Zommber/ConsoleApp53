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

    // Hace la conversión entre dos monedas.
    public class ConvertidorMoneda
    {
        private Moneda monedaOrigen;
        private Moneda monedaDestino;

        public ConvertidorMoneda(Moneda origen, Moneda destino)
        {
            monedaOrigen = origen;
            monedaDestino = destino;
        }

        // Convierte usando la tasa relativa.
        public double Convertir(double cantidad)
        {
            double enDolares = cantidad / monedaOrigen.GetTasaCambio();
            return enDolares * monedaDestino.GetTasaCambio();
        }

        public void MostrarConversion(double cantidad)
        {
            double resultado = Convertir(cantidad);
            Console.WriteLine();
            Console.WriteLine("Resultado:");
            Console.WriteLine($"{cantidad:F2} {monedaOrigen.GetSimbolo()} = {resultado:F2} {monedaDestino.GetSimbolo()}");
            Console.WriteLine($"1 {monedaOrigen.GetNombre()} = {(monedaDestino.GetTasaCambio() / monedaOrigen.GetTasaCambio()):F4} {monedaDestino.GetNombre()}");
        }

        public void IntercambiarMonedas()
        {
            var tmp = monedaOrigen;
            monedaOrigen = monedaDestino;
            monedaDestino = tmp;
            Console.WriteLine("Se intercambiaron las monedas.");
        }
    }

    class Program
    {
        // Lee un número (acepta coma o punto). Devuelve true si se leyó bien.
        static bool TryLeerDouble(string prompt, out double valor)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            valor = 0;
            if (string.IsNullOrWhiteSpace(input)) return false;

            // Primero intenta con la cultura actual, luego con invariante (para aceptar '.' o ',').
            if (double.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out valor)) return true;
            if (double.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out valor)) return true;
            return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("CONVERTIDOR DE PESOS A DÓLARES");
            Console.WriteLine();

            // Valores por defecto
            Moneda peso = new Moneda("Peso Mexicano", "MXN", 20.0);
            Moneda dolar = new Moneda("Dólar Estadounidense", "USD", 1.0);

            var convertidor = new ConvertidorMoneda(peso, dolar);

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine();
                Console.WriteLine("MENÚ:");
                Console.WriteLine("1. Convertir Pesos a Dólares");
                Console.WriteLine("2. Convertir Dólares a Pesos");
                Console.WriteLine("3. Actualizar tasa (pesos por 1 USD)");
                Console.WriteLine("4. Ver info de monedas");
                Console.WriteLine("5. Salir");
                Console.Write("Opción: ");

                string opcion = Console.ReadLine() ?? "";
                switch (opcion)
                {
                    case "1":
                        if (TryLeerDouble("Cantidad en pesos: ", out double cps))
                        {
                            if (cps < 0) Console.WriteLine("No se aceptan cantidades negativas.");
                            else convertidor.MostrarConversion(cps);
                        }
                        else
                        {
                            Console.WriteLine("Entrada inválida. Escribe un número como 100 o 100.50");
                        }
                        break;
                    case "2":
                        convertidor.IntercambiarMonedas();
                        if (TryLeerDouble("Cantidad en dólares: ", out double cds))
                        {
                            if (cds < 0) Console.WriteLine("No se aceptan cantidades negativas.");
                            else convertidor.MostrarConversion(cds);
                        }
                        else
                        {
                            Console.WriteLine("Entrada inválida. Escribe un número válido.");
                        }
                        convertidor.IntercambiarMonedas();
                        break;

                    case "3":
                        if (TryLeerDouble("Nueva tasa (pesos por 1 USD): ", out double nt))
                        {
                            peso.SetTasaCambio(nt);
                            Console.WriteLine($"Tasa actualizada a {nt:F2} MXN = 1 USD");
                        }
                        else
                        {
                            Console.WriteLine("Entrada inválida.");
                        }
                        break;
                    case "4":
                        Console.WriteLine();
                        peso.MostrarInfo();
                        Console.WriteLine();
                        dolar.MostrarInfo();
                        break;

                    case "5":
                        continuar = false;
                        Console.WriteLine("Gracias. Hasta luego.");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
    }
}
