using Products;

namespace Store;

public interface IWindow<T> where T : IProduct
{
    int Id { get; set; }
    protected T?[] ProductList { get; set; }

    public T? this[int index] { get; set; }

    public void Push(T product);

    public void Insert(int index, T product);

    public void Pop();

    public void Remove(int idx);

    public void SwapByIndex(int first, int second);

    public int IndexOf(Predicate<T>? predicate);

    public int IndexOfById(int id);

    public T? FindById(int productId);

    public T? FindByName(string productName);

    private int CompareById(T? lhs, T? rhs)
    {
        if (lhs is null && rhs is null)
        {
            return 0;
        }
        else if (lhs is null) return 1;
        else if (rhs is null) return -1;
        else return lhs.Id == rhs.Id ? 0 : 1;
    }

    private int CompareByName(T? lhs, T? rhs)
    {
        if (lhs is null && rhs is null)
        {
            return 0;
        }
        else if (lhs is null) return 1;
        else if (rhs is null) return -1;
        else return lhs.Name == rhs.Name ? 0 : 1;
    }

    public void SortById();
    public void SortByName();
}