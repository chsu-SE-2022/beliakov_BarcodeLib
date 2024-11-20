using BarcodeLib;

namespace Products;

public interface IProduct
{
    public int Id { get; }
    public string Name { get; }
    public IBarcode Barcode { get; }
}