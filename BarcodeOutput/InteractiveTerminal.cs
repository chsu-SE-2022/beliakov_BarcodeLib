using Products;
using Store;

namespace BarcodeOutput;

public static class InteractiveTerminal
{
    private static IProduct? _currentProduct;
    // ReSharper disable once InconsistentNaming
    private const int WINDOW_SIZE = 20;
    private static Window<Router> _routerWindow = WINDOW_SIZE;
    private static Window<Switch> _switchWindow = WINDOW_SIZE;
    private static Window<IProduct> _genericWindow = WINDOW_SIZE;
    public static void Repl(string? input)
    {
        int index;
        int id;
        switch (input)
        {
                case "CreateRouter":
                    List<string> routerParams = CreateRouterParameters();
                    _currentProduct = CreateRouter(routerParams);
                    break;
                case "CreateSwitch":
                    List<string> switchParams = CreateSwitchParameters();
                    _currentProduct = CreateSwitch(switchParams);
                    break;
                case "SelectProduct":
                    SelectProduct(ChooseWindow(), ChooseIndex());
                    break;
                case "PrintGenericWindow":
                    Console.WriteLine(_genericWindow);
                    break;
                case "PrintSwitchWindow":
                    Console.WriteLine(_switchWindow);
                    break;
                case "PrintRouterWindow":
                    Console.WriteLine(_routerWindow);
                    break;
                case "PrintProduct":
                    Console.WriteLine(_currentProduct);
                    break;
                case "ChangeProductID":
                    if (_currentProduct == null)
                    {
                        Console.WriteLine("No product selected, select one first");
                        break;
                    }
                    Console.WriteLine("Choose ID");
                    id = int.Parse(Console.ReadLine() ?? "0");
                    _currentProduct.Id = id;
                    break;
                case "ChangeWindowID":
                    ChangeWindowId(ChooseWindow());
                    break;
                case "PushCurrentProduct":
                    PushCurrentProduct(ChooseWindow());
                    break;
                case "InsertCurrentProduct":
                    InsertCurrentProduct(ChooseWindow(), ChooseIndex());
                    break;
                case "Pop":
                    Pop(ChooseWindow());
                    break;
                case "Remove":
                    Remove(ChooseWindow(), ChooseIndex());
                    break;
                case "SwapByIndex":
                    SwapByIndex(ChooseWindow(), ChooseIndex(), ChooseIndex());
                    break;
                case "IndexOf":
                    IndexOf(ChooseWindow());
                    break;
                case "FindProductById":
                    _currentProduct = FindById(ChooseWindow());
                    break;
                case "FindProductByName":
                    _currentProduct = FindByName(ChooseWindow());
                    break;
                case "SortById":
                    SortById(ChooseWindow());
                    break;
                case "SortByName":
                    SortByName(ChooseWindow());
                    break;
                case null:
                    Console.WriteLine("Error: empty output");
                    break;
                case "Quit":
                    return;
        }
    }

    private static void SortByName(string? windowName)
    {
        switch (windowName)
        {
            case "router":
                _routerWindow.SortByName();
                break;
            case "switch":
                _switchWindow.SortByName();
                break;
            case "generic":
                _genericWindow.SortByName();
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    private static void SortById(string? windowName)
    {
        switch (windowName)
        {
            case "router":
                _routerWindow.SortById();
                break;
            case "switch":
                _switchWindow.SortById();
                break;
            case "generic":
                _genericWindow.SortById();
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    private static IProduct? FindByName(string? windowName)
    {
        Console.WriteLine("Choose a name");
        var name = Console.ReadLine() ?? "0";

        switch (windowName)
        {
            case "router":
                return _routerWindow.FindByName(name);
            case "switch":
                return _switchWindow.FindByName(name);
            case "generic":
                return _genericWindow.FindByName(name);
            default:
                Console.WriteLine("Invalid input");
                return null;
        }
    }

    private static IProduct? FindById(string? windowName)
    {
        Console.WriteLine("Choose id");
        var id = int.Parse(Console.ReadLine() ?? "0");

        switch (windowName)
        {
            case "router":
                return _routerWindow.FindById(id);
            case "switch":
                return _switchWindow.FindById(id);
            case "generic":
                return _genericWindow.FindById(id);
            default:
                Console.WriteLine("Invalid input");
                return null;
        }
    }

    private static void IndexOf(string? windowName)
    {
        Console.WriteLine("Choose id");
        var id = int.Parse(Console.ReadLine() ?? "0");

        int? index = -1;
        switch (windowName)
        {
            case "router":
                index = _routerWindow.IndexOfById(id);
                break;
            case "switch":
                index = _switchWindow.IndexOfById(id);
                break;
            case "generic":
                index = _genericWindow.IndexOfById(id);
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
        Console.WriteLine(index);
    }

    private static string? ChooseWindow()
    {
        Console.WriteLine("Choose the window");
        var windowName = Console.ReadLine()?.ToLower();
        if (windowName == "router" || windowName == "switch" || windowName == "generic")
        {
            return windowName;
        }
        else
        {
            return null;
        }
    }

    private static int ChooseIndex()
    {
        Console.WriteLine("Choose the index");
        var index = int.Parse(Console.ReadLine() ?? "0");
        if (index < 0 || index >= WINDOW_SIZE)
        {
            Console.WriteLine("Index out of bounds, using 0");
            return 0;
        }

        return index;
    }

    private static void SwapByIndex(string? windowName, int lhsIndex, int rhsIndex)
    {
        switch (windowName)
        {
            case "router":
                _routerWindow.SwapByIndex(lhsIndex, rhsIndex);
                break;
            case "switch":
                _switchWindow.SwapByIndex(lhsIndex, rhsIndex);
                break;
            case "generic":
                _genericWindow.SwapByIndex(lhsIndex, rhsIndex);
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    private static void Remove(string? windowName, int index)
    {
        switch (windowName)
        {
            case "router":
                _routerWindow.Remove(index);
                break;
            case "switch":
                _switchWindow.Remove(index);
                break;
            case "generic":
                _genericWindow.Remove(index);
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    private static void Pop(string? windowName)
    {
        switch (windowName)
        {
            case "router":
                _routerWindow.Pop();
                break;
            case "switch":
                _switchWindow.Pop();
                break;
            case "generic":
                _genericWindow.Pop();
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    public static void InsertCurrentProduct(string? windowName, int index)
    {
        if (_currentProduct == null)
        {
            Console.WriteLine("No product selected, select one first");
            return;
        }
        switch (windowName)
        {
            case "router":
                if (_currentProduct is Router router)
                {
                    _routerWindow.Insert(index, router);
                }
                else
                {
                    Console.WriteLine("Error: current product is not a router");
                }
                break;
            case "switch":
                if (_currentProduct is Switch sSwitch)
                {
                    _switchWindow.Insert(index, sSwitch);
                }
                else
                {
                    Console.WriteLine("Error: current product is not a switch");
                }
                break;
            case "generic":
                _genericWindow.Insert(index, _currentProduct);
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }
    private static void PushCurrentProduct(string? windowName)
    {
        if (_currentProduct == null)
        {
            Console.WriteLine("No product selected, select one first");
            return;
        }
        switch (windowName)
        {
            case "router":
                if (_currentProduct is Router router)
                {
                    _routerWindow.Push(router);
                }
                else
                {
                    Console.WriteLine("Error: current product is not a router");
                }
                break;
            case "switch":
                if (_currentProduct is Switch sSwitch)
                {
                    _switchWindow.Push(sSwitch);
                }
                else
                {
                    Console.WriteLine("Error: current product is not a switch");
                }
                break;
            case "generic":
                _genericWindow.Push(_currentProduct);
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    private static void ChangeWindowId(string? windowName)
    {
        Console.WriteLine("Choose ID");
        int id = int.Parse(Console.ReadLine() ?? "0");
        switch (windowName)
        {
            case "router":
                _routerWindow.Id = id;
                break;
            case "switch":
                _switchWindow.Id = id;
                break;
            case "generic":
                _genericWindow.Id = id;
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    public static void SelectProduct(string? windowName, int index)
    {
        Console.WriteLine("Choose the window (router, switch, generic)");
        switch (Console.ReadLine()?.ToLower())
        {
            case "router":
                _currentProduct = _routerWindow[index];
                break;
            case "switch":
                _currentProduct = _switchWindow[index];
                break;
            case "generic":
                _currentProduct = _genericWindow[index];
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    public static List<string> CreateSwitchParameters()
    {
        Console.WriteLine("Choose a switch ID");
        var switchId = Console.ReadLine() ?? "0";
        Console.WriteLine("Choose a switch name");
        var switchName = Console.ReadLine() ?? "";
        Console.WriteLine("Choose a switch model");
        var switchModel = Console.ReadLine() ?? "";
        Console.WriteLine("Choose a switch price");
        var switchPrice = Console.ReadLine() ?? "0";
        Console.WriteLine("Choose a switch supported bandwidth");
        var switchBandwidth = Console.ReadLine() ?? "0";        
        Console.WriteLine("Choose a switch manufacturer");
        var switchManufacturer = Console.ReadLine() ?? "";

        Console.WriteLine("Choose a switch supported device count");
        var switchDeviceCount = Console.ReadLine() ?? "0";
        return
        [
            switchId,
            switchName,
            switchModel,
            switchPrice,
            switchBandwidth,
            switchManufacturer,
            switchDeviceCount
        ];
    }
    
    public static Switch CreateSwitch(List<string> switchParameters)
    {
        var switchId = int.Parse(switchParameters[0]);
        var switchName = switchParameters[1];
        var switchModel = switchParameters[2];
        var switchPrice = decimal.Parse(switchParameters[3]);
        var switchBandwidth = decimal.Parse(switchParameters[4]);
        var switchManufacturer = switchParameters[5];
        var switchDeviceCount = int.Parse(switchParameters[6]);
        var newSwitch = new Switch(switchId, switchName, switchModel, switchPrice, switchBandwidth, switchManufacturer, switchDeviceCount);
        return newSwitch;
    }

    public static List<string> CreateRouterParameters()
    {
        Console.WriteLine("Choose a router ID");
        var routerId = Console.ReadLine() ?? "0";
        Console.WriteLine("Choose a router name");
        var routerName = Console.ReadLine() ?? "";
        Console.WriteLine("Choose a router model");
        var routerModel = Console.ReadLine() ?? "";
        Console.WriteLine("Choose a router price");
        var routerPrice = Console.ReadLine() ?? "0";
        Console.WriteLine("Choose a router bandwidth");
        var routerBandwidth = Console.ReadLine() ?? "0";
        return
        [
            routerId,
            routerName,
            routerModel,
            routerPrice,
            routerBandwidth
        ];
    }
    public static Router CreateRouter(List<string> routerParameters)
    {

        var routerId = int.Parse(routerParameters[0]);
        var routerName = routerParameters[1];
        var routerModel = routerParameters[2];
        var routerPrice = decimal.Parse(routerParameters[3]);
        var routerBandwidth = decimal.Parse(routerParameters[4]);
                    
        var newRouter = new Router(routerId, routerName, routerModel, routerPrice, routerBandwidth);
        return newRouter;
    }
}