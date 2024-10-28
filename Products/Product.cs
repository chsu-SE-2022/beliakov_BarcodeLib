using BarcodeLib;

namespace Products;

public abstract class Product
{
    protected int id;

    protected string? Type;

    public int Id
    {
        get => id;
        set
        {
            if (value == id) return;
            id = value;
            Barcode.InitialString = id.ToString();
        }
    }
    public string Name { get; protected set; }
    protected Barcode Barcode { get; }

    public void ChangeBarcodeText(string text)
    {
        Barcode.InitialString = text;
    }

    protected Product(int id, string name)
    {
        Barcode = new Barcode(id.ToString());
        Id = id;
        Name = name;
    }


    public override string ToString()
    {
        return $"Id:            {Id}\n" +
               $"Type:          {Type}\n" +
               $"Name:          {Name}\n" +
               $"Barcode:\n{Barcode}\n";
    }
}