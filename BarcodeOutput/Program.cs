using BarcodeLib;
using Products;
using Store;

namespace BarcodeOutput;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Commands:\n" +
                              "CreateRouter "+
                              "CreateSwitch "+
                              "SelectProduct\n"+
                              "PrintGenericWindow "+
                              "PrintSwitchWindow "+
                              "PrintRouterWindow "+
                              "PrintProduct\n"+
                              "ChangeProductID "+
                              "ChangeWindowID "+
                              "PushCurrentProduct "+
                              "InsertCurrentProduct\n"+
                              "Pop "+
                              "Remove "+
                              "SwapByIndex "+
                              "IndexOf "+
                              "FindProductById "+
                              "FindProductByName\n"+
                              "SortById "+
                              "SortByName "+
                              "Quit\n");
            var input = Console.ReadLine();
            if (input == "Exit")
            {
                break;
            }
            else
            {
                InteractiveTerminal.Repl(input);
            }
        }
    }

    

    static void Test()
    {
        var lab4Data = new List<IProduct>()
        {
            new Router(10, "FirstR", "FRST", 6000.0m, 120.0m),
            new Router(11, "SecondR", "SCND", 2.0m, 1.0m),
            new Router(12, "ThirdR", "THRD", 22000.0m, 140.0m),
            new Switch(13, "FirstS", "FS", 2.0m, 1, "text", 10),
            new Switch(14, "SecondS", "SS", 2.0m, 1, "text", 10),
            new Switch(15, "ThirdS", "TS", 2.0m, 1, "text", 10)
        };

        Window<IProduct> w = (7, 1);

        foreach (var product in lab4Data)
        {
            w.Push(product);
        }
        w.Id++;
        w.Id++;
        w.Id++;
        w.Id++;
        w.Id++;
    }

    static void TestLab4()
    {
        Console.Clear();
        var lab4Data = new List<IProduct>()
        {
            new Router(10, "FirstR", "FRST", 6000.0m, 120.0m),
            new Router(11, "SecondR", "SCND", 2.0m, 1.0m),
            new Router(12, "ThirdR", "THRD", 22000.0m, 140.0m),
            new Switch(13, "FirstS", "FS", 22000.0m, 1, "text", 10),
            new Switch(14, "SecondS", "SS", 22000.0m, 1, "text", 10),
            new Switch(15, "ThirdS", "TS", 22000.0m, 1, "text", 10)
        };

        Window<IProduct> window = (7, 1);

        foreach (var product in lab4Data)
        {
            window.Push(product);
        }
        window.SortByName();

        var sample1 = new Switch(16, "FourthS", "FS", 22000.0m, 1, "text", 10);
        var sample2 = new Router(12, "SixthR", "SRD", 22000.0m, 140.0m);
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
            new Router(10, "FirstR", "FRST", 6000.0m, 120.0m),
            new Router(11, "SecondR", "SCND", 2.0m, 1.0m),
            new Router(12, "ThirdR", "THRD", 22000.0m, 140.0m)
        };

        var lab3Data2 = new List<Switch>()
        {
            new Switch(13, "FirstS", "FS", 6000.0m, 1, "text", 10),
            new Switch(14, "SecondS", "SS", 6000.0m, 1, "text", 10),
            new Switch(15, "ThirdS", "TS", 6000.0m, 1, "text", 10)
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
        var sample1 = new Switch(16, "CustomSwitch", "AWM", 22000.0m, 20, "text", 10);
        var sample2 = new Router(10, "CustomRouter", "DPL", 22000.0m, 140.0m);

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

        var sample = new Router(10, "Sample", "SPL", 12000.0m, 120.0m);
        var sampleList = new List<Product>
        {
            new Router(10, "Somple", "SOPL", 6000.0m, 120.0m),
            new Router(10, "S1mple", "S1PL", 2.0m, 1.0m),
            new Router(10, "Dimple", "DPL", 22000.0m, 140.0m)
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