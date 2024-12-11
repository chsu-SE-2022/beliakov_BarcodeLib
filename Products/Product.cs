using BarcodeLib;

namespace Products;
/// <summary>
/// Model-class for all Products (Router, Switch)
/// </summary>
/// <param name="id">Integer ID of the product. Supposed to be unique</param>
/// <param name="name">Basic product name</param>
public abstract class Product(int id, string name) : IProduct
{
    private int _id = id;
    protected string? Type;
    public event EventHandler<IdChangeArg>? OnIdChanged;

    public int Id
    {
        get => _id;
        set
        {
            if (value == _id) return;
            IdChangeArg args = new() { OldId = _id, NewId = value };
            _id = value;
            Barcode.InitialString = _id.ToString();
            OnIdChanged?.Invoke(this, args);
        }
    }
    public string Name { get; protected set; } = name;
    public abstract IBarcode Barcode { get; }

    public virtual void ChangeBarcodeText(string text) => Barcode.InitialString = text;

    public override string ToString()
    {
        return $"Id:            {Id}\n" +
               $"Type:          {Type}\n" +
               $"Name:          {Name}\n" +
               $"Barcode:\n{Barcode.ToString()}\n";
    }
}