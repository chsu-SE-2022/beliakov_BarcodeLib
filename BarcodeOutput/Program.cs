using BarcodeLib;
using Products;
using Store;

namespace BarcodeOutput;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("".PadLeft(80, '='));
        Window showcase = 5;
        
        var sample = new Router(10, "Sample", "SPL", 12000.0m, 120.0m, "Balls");
        var sampleList = new List<Product>
        {
            new Router(10, "Somple", "SOPL", 6000.0m, 120.0m, "Malls"),
            new Router(10, "S1mple", "S1PL", 2.0m, 1.0m, "Calls"),
            new Router(10, "Dimple", "DPL", 22000.0m, 140.0m, "Balls")
        };

        foreach (var product in sampleList)
        {
            showcase.Add(product);
        }

        showcase[4] = sample;

        sample.Id++;
        Console.WriteLine(sample);
        
        showcase.SortByName();

        showcase.Id++;
        Console.WriteLine(showcase);
    }
}