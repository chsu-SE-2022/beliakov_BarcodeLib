using BarcodeLib;
using Products;
using Store;

namespace BarcodeOutput;

class Program
{
    static void Main(string[] args)
    {
        TestLab3();
    }

    static void TestLab4()
    {
        Console.Clear();
        var lab4Data = new List<IProduct>()
        {
            new Router(10, "FirstR", "FRST", 6000.0m, 120.0m, "Malls"),
            new Router(11, "SecondR", "SCND", 2.0m, 1.0m, "Calls"),
            new Router(12, "ThirdR", "THRD", 22000.0m, 140.0m, "Balls"),
            new Switch(13, "FirstS", "FS", "Navi", 1, "text"),
            new Switch(14, "SecondS", "SS", "Navi", 1, "text"),
            new Switch(15, "ThirdS", "TS", "Navi", 1, "text")
        };

        Window<IProduct> window = (7, 1);

        foreach (var product in lab4Data)
        {
            window.Push(product);
        }
        window.SortByName();

        var sample1 = new Switch(16, "FourthS", "FS", "Navi", 1, "text");
        var sample2 = new Router(12, "SixthR", "SRD", 22000.0m, 140.0m, "Balls");
        window[5] = sample1;
        window[6] = sample2;

        window.Id = 2;
        sample1.Id++;
        sample2.Id++;

        Console.WriteLine(window);


    }

    static void TestLab3()
    {
        Console.Clear();
        var lab3Data = new List<IProduct>()
        {
            new Router(10, "FirstR", "FRST", 6000.0m, 120.0m, "Malls"),
            new Router(11, "SecondR", "SCND", 2.0m, 1.0m, "Calls"),
            new Router(12, "ThirdR", "THRD", 22000.0m, 140.0m, "Balls")
        };

        var lab3Data2 = new List<Switch>()
        {
            new Switch(13, "FirstS", "FS", "Navi", 1, "text"),
            new Switch(14, "SecondS", "SS", "Navi", 1, "text"),
            new Switch(15, "ThirdS", "TS", "Navi", 1, "text")
        };

        Window<IProduct> genericWindow1 = 7;
        IWindow<IProduct> genericWindow = genericWindow1;
        genericWindow.Id++;

        Window<Switch> switchWindow = (3, 10);

        foreach (var product in lab3Data)
        {
            genericWindow.Push(product);
        }

        foreach (var product in lab3Data2)
        {
            switchWindow.Push(product);
        }
        genericWindow.SortByName();
        var sample1 = new Switch(16, "CustomSwitch", "AWM", "AwesomeManu", 20, "text");
        var sample2 = new Router(10, "CustomRouter", "DPL", 22000.0m, 140.0m, "Balls");

        switchWindow[0] = sample1;
        genericWindow[5] = switchWindow[0];
        genericWindow[6] = sample2;

        genericWindow.Id = 2;
        sample1.Id++;
        sample2.Id++;

        Console.WriteLine(genericWindow);

        if (genericWindow[5] is Switch sw)
        {
            switchWindow[0] = sw;
        }

        Console.WriteLine(switchWindow);
    }

    static void TestLab2()
    {
        Console.WriteLine("".PadLeft(80, '='));
        Window<Product> showcase = 5;

        var sample = new Router(10, "Sample", "SPL", 12000.0m, 120.0m, "Balls");
        var sampleList = new List<Product>
        {
            new Router(10, "Somple", "SOPL", 6000.0m, 120.0m, "Malls"),
            new Router(10, "S1mple", "S1PL", 2.0m, 1.0m, "Calls"),
            new Router(10, "Dimple", "DPL", 22000.0m, 140.0m, "Balls")
        };

        foreach (var product in sampleList)
        {
            showcase.Push(product);
        }

        showcase[4] = sample;

        sample.Id++;
        Console.WriteLine(sample);

        showcase.SortByName();

        showcase.Id++;
        Console.WriteLine(showcase);
    }
}