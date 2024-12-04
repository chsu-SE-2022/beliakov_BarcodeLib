using BarcodeLib;
using Products;
using Store;

namespace BarcodeOutput;

class Program
{
    static void Main(string[] args)
    {
        REPL();
    }

    static void REPL()
    {
        Window<Product>? currentWindow = null;
        Product? currentProduct = null;
        
        List<Window<Product>> windows = new List<Window<Product>>();
        while (true)
        {
            Console.WriteLine("Commands:\n" +
                              "CreateWindow, " +
                              "CreateRouter, " +
                              "CreateSwitch\n" +
                              "SelectWindow, " +
                              "SelectProduct\n" +
                              "PrintWindow," +
                              "PrintProduct\n" +
                              "ChangeProductID, " +
                              "ChangeWindowID");
            string? input = Console.ReadLine();
            int index;
            int id;
            switch (input)
            {
                case "CreateWindow":
                    Console.WriteLine("Choose a window ID (skip to use the next incremental one)");
                    string? windowId = Console.ReadLine();
                    Console.WriteLine("Choose a window size");
                    string size = Console.ReadLine() ?? "10";
                    if (windowId != null)
                    {
                        Window<Product> newWindow = (int.Parse(size), int.Parse(windowId));
                        windows.Add(newWindow);
                        currentWindow = newWindow;
                    }
                    else
                    {
                        Window<Product> newWindow = int.Parse(size);
                        windows.Add(newWindow);
                        currentWindow = newWindow;
                    }

                    break;
                case "CreateRouter":
                    Console.WriteLine("Choose a router ID");
                    int routerId = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine("Choose a router name");
                    string routerName = Console.ReadLine() ?? "";
                    Console.WriteLine("Choose a router model");
                    string routerModel = Console.ReadLine() ?? "";
                    Console.WriteLine("Choose a router price");
                    decimal routerPrice = decimal.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine("Choose a router bandwidth");
                    decimal routerBandwidth = decimal.Parse(Console.ReadLine() ?? "0");                     
                    Console.WriteLine("Choose router's wifi standard");
                    string routerStandards = Console.ReadLine() ?? ""; 
                    Router newRouter = new Router(routerId, routerName, routerModel, routerPrice, routerBandwidth, routerStandards);
                    currentProduct = newRouter;
                    Console.WriteLine("Add it to current window? y/n");
                    switch (Console.ReadLine())
                    {
                        case "y":
                            currentWindow.Push(newRouter);
                            break;
                        case "n":
                            break;
                    }

                    break;
                case "CreateSwitch":
                    Console.WriteLine("Choose a switch ID");
                    int switchId = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine("Choose a switch name");
                    string switchName = Console.ReadLine() ?? "";
                    Console.WriteLine("Choose a switch model");
                    string switchModel = Console.ReadLine() ?? "";
                    Console.WriteLine("Choose a switch manufacturer");
                    string switchManufacturer = Console.ReadLine() ?? "";
                    Console.WriteLine("Choose a switch supported device count");
                    int switchDeviceCount = int.Parse(Console.ReadLine() ?? "0");
                    Switch newSwitch = new Switch(switchId, switchName, switchModel, switchManufacturer, switchDeviceCount, switchId.ToString());
                    currentProduct = newSwitch;
                    Console.WriteLine("Add it to current window? y/n");
                    switch (Console.ReadLine())
                    {
                        case "y":
                            currentWindow.Push(newSwitch);
                            break;
                        case "n":
                            break;
                    }

                    break;
                case "SelectWindow":
                    Console.WriteLine("Choose index");
                    index = int.Parse(Console.ReadLine() ?? "0");
                    currentWindow = windows[index];
                    break;
                case "SelectProduct":
                    Console.WriteLine("Choose index");
                    index = int.Parse(Console.ReadLine() ?? "0");
                    currentProduct = currentWindow[index];
                    break;
                case "PrintWindow":
                    Console.WriteLine(currentWindow);
                    break;
                case "PrintProduct":
                    Console.WriteLine(currentProduct);
                    break;
                case "ChangeProductID":
                    Console.WriteLine("Choose ID");
                    id = int.Parse(Console.ReadLine() ?? "0");
                    currentProduct.Id = id;
                    break;
                case "ChangeWindowID":
                    Console.WriteLine("Choose ID");
                    id = int.Parse(Console.ReadLine() ?? "0");
                    currentWindow.Id = id;
                    break;
                case null:
                    Console.WriteLine("Error: empty output");
                    break;
                case "Quit":
                    return;
            }
        }
    }

    static void Test()
    {
        var lab4Data = new List<IProduct>()
        {
            new Router(10, "FirstR", "FRST", 6000.0m, 120.0m, "Malls"),
            new Router(11, "SecondR", "SCND", 2.0m, 1.0m, "Calls"),
            new Router(12, "ThirdR", "THRD", 22000.0m, 140.0m, "Balls"),
            new Switch(13, "FirstS", "FS", "Navi", 1, "text"),
            new Switch(14, "SecondS", "SS", "Navi", 1, "text"),
            new Switch(15, "ThirdS", "TS", "Navi", 1, "text")
        };

        Window<IProduct> w = (7, 1);

        foreach (var product in lab4Data)
        {
            w.Push(product);
        }

        w.OnUpdate = Console.WriteLine;
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