using System.Text;
using Products;

namespace Store;

public class Window<T> : IWindow<T> where T : class, IProduct
{
    protected static int CurrentId = 0;

    public Action<Window<T>> OnUpdate {get; set;}

    public void OnIdChangeHandler(object? sender, IdChangeArg args)
    {
        Console.WriteLine("OnIdChangeHandler");
        if (args.NewId != null && sender?.GetType() == typeof(T))
        {
            ChangeBarcodeText((T)sender, args.NewId.Value);
        }
    }

    private int _id;

    public T?[] ProductList { get; set; }

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            CurrentId = value + 1;
            OnUpdate(this);
            UpdateAllBarcodes();

        }
    }

    protected Window(int count)
    {
        OnUpdate = window => { };
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
            if (value != null)
            {
                value.OnIdChanged += OnIdChangeHandler;
            };
            ProductList[index] = value;
            // ChangeBarcodeText(value, index);
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
        product.OnIdChanged += OnIdChangeHandler;
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
    public T? Find(Predicate<T?> func)
    {
        return Array.Find(ProductList, func);
    }

    public T? FindById(int productId)
    {
        return Find((pr) => pr?.Id == productId);
    }

    public T? FindByName(string productName)
    {
        return Find((pr) => pr?.Name == productName);
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
    public void Sort(Comparison<T?> compare)
    {
        Array.Sort(ProductList, compare);
        UpdateAllBarcodes();
    }

    public void SortById()
    {
        Sort(CompareById);
    }
    public void SortByName()
    {
        Sort(CompareByName);
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