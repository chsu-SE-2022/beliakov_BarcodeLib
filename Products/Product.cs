using BarcodeLib;

namespace Products;

public abstract class Product : IProduct
{
    private int id;
    protected string? Type;
    public event EventHandler<IdChangeArg>? OnIdChanged;

    private IdChangeArg? OnIdChangeArg { get; set; }


    public int Id
    {
        get => id;
        set
        {
            if (value == id) return;
            OnIdChangeArg.OldId = OnIdChangeArg.NewId;
            OnIdChangeArg.NewId = value;
            if (OnIdChanged != null)
            {
                OnIdChanged.Invoke(this, OnIdChangeArg);
            }
            id = value;
            Barcode.InitialString = id.ToString();
        }
    }
    public string Name { get; protected set; }
    public abstract IBarcode Barcode { get; }

    public virtual void ChangeBarcodeText(string text) => Barcode.InitialString = text;

    protected Product(int id, string name)
    {
        OnIdChangeArg = new IdChangeArg
        {
            OldId = null,
            NewId = id
        };
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