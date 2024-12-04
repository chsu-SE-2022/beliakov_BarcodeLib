using System.Text;
using Products;

namespace Store;



public class Window<T> : IWindow<T> where T : class, IProduct
{

    public delegate void OnWindowIdUpdate(IWindow<T> window);

    protected static int CurrentId = 0;

    private OnWindowIdUpdate _onWindowIdUpdate;

    private void OnIdChangeHandler(object? sender, IdChangeArg args)
    {
        // Console.WriteLine($"Id changed from {args.OldId} to {args.NewId}");
        if (args.NewId != null && sender?.GetType() == typeof(T))
        {
            ((T)sender).OnProductIdUpdate(this);
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
            _onWindowIdUpdate?.Invoke(this);
        }
    }

    private Window(int count)
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
            var ret = ProductList[index];
            if (ret != null)
            {
                ret.OnIdChanged -= OnIdChangeHandler;
                _onWindowIdUpdate -= ret.OnProductIdUpdate;
            }

            ProductList[index] = null;
            return ret;
        }
        set
        {
            if (index < 0 || index >= ProductList.Length) return;
            if (value != null)
            {
                value.OnIdChanged += OnIdChangeHandler;
                _onWindowIdUpdate += value.OnProductIdUpdate;
            };
            ProductList[index] = value;
        }
    }
    //
    // protected void OnWindowIdUpdate()
    // {
    //     foreach (var product in ProductList)
    //     {
    //         product?.OnProductIdUpdate(this);
    //     }
    // }

    // protected void ChangeBarcodeText(T? value, int index)
    // {
    //     if (value == null) return;
    //     value.Barcode.InitialString = ($"{value.Id} {Id} {index}");
    // }

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
        Remove(0);
    }

    public void Remove(int idx)
    {
        this[idx] = default;
    }

    public void SwapByIndex(int first, int second)
    {
        (this[first], this[second]) = (this[second], this[first]);
    }

    public int IndexOf(Predicate<T>? predicate)
    {
        return Array.IndexOf(ProductList, predicate);
    }

    public int IndexOfById(int id)
    {
        return IndexOf(pr => pr?.Id == id);
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
        _onWindowIdUpdate(this);
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