using System.Text;
using Products;

namespace Store;

public class Window
{
    private static int CurrentId = 1;

    private int id;

    private int Id
    {
        get => id;
        set
        {
            id = value;
            CurrentId = value + 1;
            foreach (var product in Products)
            {
                product?.ChangeBarcodeText($"{product.Id} {value} {Array.IndexOf(Products, product)}");
            }
        }
    }

    private Window(int count)
    {
        Products = new Product?[count];
        Id = CurrentId;
        CurrentId += 1;
    }

    public static implicit operator Window(int count)
    {
        return new Window(count);
    }

    private Product?[] Products { get; set; }

    public Product? this[int index]
    {
        get
        {
            if (index < 0 || index >= Products.Length) return null;
            return Products[index];
        }
        set
        {
            if (index < 0 || index >= Products.Length) return;
            Products[index] = value;
        }
    }

    public void Add(Product product)
    {
        int firstEmpty = Array.FindIndex(Products, (pr) => pr == null);
        product.ChangeBarcodeText($"{product.Id} {Id} {firstEmpty}");
        Products[firstEmpty] = product;

    }

    public void Insert(int index, Product product)
    {
        product.ChangeBarcodeText($"{product.Id} {Id} {index}");
        Products[index] = product;
    }

    public void Pop()
    {
        Products[0] = null;
    }

    public void Remove(int idx)
    {
        Products[idx] = null;
    }

    public Product? FindById(int productId)
    {
        return Array.Find(Products, (pr) => pr?.Id == productId);
    }

    public Product? FindByName(string productName)
    {
        return Array.Find(Products, (pr) => pr?.Name == productName);
    }

    private int CompareById(Product? lhs, Product? rhs)
    {
        if (lhs is null && rhs is null)
        {
            return 0;
        }
        else if (lhs is null) return 1;
        else if (rhs is null) return -1;
        else return lhs.Id == rhs.Id ? 0 : 1;
    }

    private int CompareByName(Product? lhs, Product? rhs)
    {
        if (lhs is null && rhs is null)
        {
            return 0;
        }
        else if (lhs is null) return 1;
        else if (rhs is null) return -1;
        else return lhs.Name == rhs.Name ? 0 : 1;
    }

    public void SortById()
    {
        Array.Sort(Products, CompareById);
    }
    public void SortByName()
    {
        Array.Sort(Products, CompareByName);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Id: {Id}");
        foreach (var p in Products)
        {
            if (p != null)
            {
                sb.AppendLine(p.ToString());
            }
            else
            {
                sb.AppendLine($"Empty\n");
            }
        }
        return sb.ToString();
    }
}