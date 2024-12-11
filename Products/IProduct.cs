using BarcodeLib;

namespace Products;

/// <summary>
/// Common interface for all Products
/// </summary>
public interface IProduct
{
    public int Id { get; set;  }
    public string Name { get; }
    public IBarcode Barcode { get; }
    public event EventHandler<IdChangeArg> OnIdChanged;
}