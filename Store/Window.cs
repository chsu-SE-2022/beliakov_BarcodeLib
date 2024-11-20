using System.Text;
using Products;

namespace Store;

public class Window<T> : IWindow<T> where T : class, IProduct
{
    protected static int CurrentId = 0;

    private int _id;

    public T?[] ProductList { get; set; }

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            CurrentId = value + 1;
            UpdateAllBarcodes();

        }
    }

    protected Window(int count)
    {
        ProductList = new T?[count];
        Id = CurrentId;
        CurrentId += 1;
    }

    public static implicit operator Window<T>(int count)
    {
        return new Window<T>(count);
    }

    public static implicit operator Window<T>((int count, int id) tuple)
    {
        CurrentId = tuple.id;
        return new Window<T>(tuple.count);
    }

    public T? this[int index]
    {
        get
        {
            if (index < 0 || index >= ProductList.Length) return null;
            return ProductList[index];
        }
        set
        {
            if (index < 0 || index >= ProductList.Length) return;
            ProductList[index] = value;
            ChangeBarcodeText(value, index);
        }
    }

    protected void UpdateAllBarcodes()
    {
        foreach (var product in ProductList)
        {
            ChangeBarcodeText(product, Array.IndexOf(ProductList, product));
        }
    }

    protected void ChangeBarcodeText(T? value, int index)
    {
        if (value == null) return;
        value.Barcode.InitialString = ($"{value.Id} {Id} {index}");
    }

    public void Push(T product)
    {
        int firstEmpty = Array.FindIndex(ProductList, (pr) => pr == null);
        this[firstEmpty] = product;

    }

    public void Insert(int index, T product)
    {
        this[index] = product;
    }

    public void Pop()
    {
        this[0] = default(T);
    }

    public void Remove(int idx)
    {
        this[idx] = default(T);
    }

    public void SwapByIndex(int first, int second)
    {
        (this[first], this[second]) = (this[second], this[first]);
    }

    public T? FindById(int productId)
    {
        return Array.Find(ProductList, (pr) => pr?.Id == productId);
    }

    public T? FindByName(string productName)
    {
        return Array.Find(ProductList, (pr) => pr?.Name == productName);
    }

    private int CompareById(T? lhs, T? rhs)
    {
        if (lhs is null && rhs is null)
        {
            return 0;
        }
        if (lhs is null) return 1;
        if (rhs is null) return -1;
        return lhs.Id == rhs.Id ? 0 : 1;
    }

    private int CompareByName(T? lhs, T? rhs)
    {
        if (lhs is null && rhs is null)
        {
            return 0;
        }
        if (lhs is null) return 1;
        if (rhs is null) return -1;
        return lhs.Name == rhs.Name ? 0 : 1;
    }

    public void SortById()
    {
        Array.Sort(ProductList, CompareByName);
        UpdateAllBarcodes();
    }
    public void SortByName()
    {
        Array.Sort(ProductList, CompareByName);
        UpdateAllBarcodes();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("".PadLeft(80, '='));

        sb.AppendLine($"Id: {Id}");
        foreach (var p in ProductList)
        {
            if (p != null)
            {
                sb.AppendLine($"Product position: {Array.IndexOf(ProductList, p) + 1}\n" + p.ToString());
            }
            else
            {
                sb.AppendLine($"Product position: {Array.IndexOf(ProductList, p) + 1}: Empty\n");
            }
        }
        return sb.ToString();
    }
}