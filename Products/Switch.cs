using BarcodeLib;

namespace Products;

public sealed class Switch : Product, IProduct
{
    IBarcode IProduct.Barcode { get; } = new BarcodeRecord("");
    public override IBarcode Barcode { get; }
    public string Model { get; private set; }
    public string Manufacturer { get; private set; }
    public int DeviceCount { get; private set; }
    public Switch(int id, string name, string model, string manufacturer, int deviceCount, string barcodeString) : base(id, name)
    {
        this.Barcode = new BarcodeRecord(id.ToString());
        base.Type = "Switch";
        Model = model;
        Manufacturer = manufacturer;
        DeviceCount = deviceCount;
        // base.Barcode.InitialString = barcodeString;
    }
    public override string ToString()
    {
        string routerString = $"{Type}:        {Name}\n" +
                              $"Model:         {Model}\n" +
                              $"Manufacturer:  {Manufacturer}\n" +
                              $"Device count:  {DeviceCount}\n";
        return routerString + base.ToString();
    }
}