using BarcodeLib;

namespace Products;

public class Router : Product
{
    private decimal Price { get; set; }
    private decimal Bandwidth { get; set; }
    private string Model { get; set; }
    public override IBarcode Barcode { get; }


    public Router(int id, string name, string model, decimal price, decimal bandwidth)
        : base(id, name)
    {
        Barcode = new Barcode(id.ToString());
        Type = "Router";
        Model = model;

        Price = price;
        Bandwidth = bandwidth;
    }

    public override string ToString()
    {
        string routerString = $"{Type}:        {Name}\n" +
                              $"Model:         {Model}\n" +
                              $"Price:         {Price}\n" +
                              $"Bandwidth:     {Bandwidth}\n";
        return routerString + base.ToString();
    }
}