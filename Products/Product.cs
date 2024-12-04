using BarcodeLib;

namespace Products;

public abstract class Product : IProduct
{
    private int id;
    protected string? Type;
    public EventHandler<IdChangeArg> OnIdChanged { get; set; }
    private IdChangeArg? OnIdChangeArg { get; set; }


    public int Id
    {
        get => id;
        set
        {
            if (value == id) return;
            OnIdChangeArg.OldId = OnIdChangeArg.NewId;
            OnIdChangeArg.NewId = value;
            OnIdChanged.Invoke(this, OnIdChangeArg);
            id = value;
            Barcode.InitialString = id.ToString();
        }
    }
    public string Name { get; protected set; }
    public abstract IBarcode Barcode { get; }
    event EventHandler<IdChangeArg>? IProduct.OnIdChanged
    {
        add => throw new NotImplementedException();
        remove => throw new NotImplementedException();
    }

    public virtual void ChangeBarcodeText(string text) => Barcode.InitialString = text;

    protected Product(int id, string name)
    {
        OnIdChangeArg = new IdChangeArg();
        OnIdChangeArg.OldId = null;
        OnIdChangeArg.NewId = id;
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