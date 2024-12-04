using Products;

namespace Store;

public static class Extensions
{
    public static void OnUpdate<T>(this T product, IWindow<T> window) where T : Product
    {
        Console.WriteLine($"Window: {product.Id}");
    }
}