using BarcodeLib;

namespace Products;

public sealed class Switch : Router, IProduct
{
    IBarcode IProduct.Barcode { get; } = new BarcodeRecord("");
    public override IBarcode Barcode { get; }
    public string Model { get; private set; }
    private string Manufacturer { get; set; }
    private int DeviceCount { get; set; }

    public override void ChangeBarcodeText(string text)
    {
    }

    public Switch(int id, string name, string model, decimal price, decimal bandwidth, string manufacturer, int deviceCount) 
        : base(id, name, model, price, bandwidth)
    {
        Barcode = new BarcodeRecord(id.ToString());
        Type = "Switch";
        Model = model;
        Manufacturer = manufacturer;
        DeviceCount = deviceCount;
    }
    public override string ToString()
    {
        string routerString = $"Manufacturer:  {Manufacturer}\n" +
                              $"Device count:  {DeviceCount}\n";
        return base.ToString() + routerString;
    }
}