using Products;

namespace Store;

public static class Extensions
{
    public static void OnProductIdUpdate<T>(this T product, IWindow<T> window) where T : IProduct
    {
        product.Barcode.InitialString = ($"{product.Id} {window.Id} {window.IndexOfById(product.Id)}");
    }
}