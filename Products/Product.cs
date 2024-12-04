using BarcodeLib;

namespace Products;

public abstract class Product : IProduct
{
    private int id;
    protected string? Type;
    public event EventHandler<IdChangeArg>? OnIdChanged;

    public int Id
    {
        get => id;
        set
        {
            if (value == id) return;
            IdChangeArg args = new() { OldId = id, NewId = value };
            id = value;
            Barcode.InitialString = id.ToString();
            OnIdChanged?.Invoke(this, args);
        }
    }
    public string Name { get; protected set; }
    public abstract IBarcode Barcode { get; }

    public virtual void ChangeBarcodeText(string text) => Barcode.InitialString = text;

    protected Product(int id, string name)
    {
        this.id = id;
        Name = name;
    }

    // protected Product(int id, string name, bool isRecord)
    // {
    //     Barcode = isRecord ? new BarcodeRecord("id") : new Barcode("id");
    //     Id = id;
    //     Name = name;
    // }


    public override string ToString()
    {
        return $"Id:            {Id}\n" +
               $"Type:          {Type}\n" +
               $"Name:          {Name}\n" +
               $"Barcode:\n{Barcode.ToString()}\n";
    }
}