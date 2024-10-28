using BarcodeLib;
using Products;
using Store;

namespace BarcodeOutput;

class Program
{
    static void Main(string[] args)
    {
        Window w = 5;

        var sample = new Router(1000, "Realtek 100", "RTK-100", 600.0m, 6.0m, "Wi-fi 6");

        var lab2Data = new List<Product>
        {
            new Router(2000, "Realtek 200", "RTK-200", 600.0m, 6.0m, "Wi-fi 6"),
            new Router(3000, "Realtek 300", "RTK-300", 600.0m, 6.0m, "Wi-fi 6"),
            new Router(4000, "Realtek 400", "RTK-400", 600.0m, 6.0m, "Wi-fi 6"),

        };
    }
}