// using System.Text;
// using Products;
//
// namespace Store;
//
// public class SwitchWindow : Window
// {
//     private int _id;
//
//     public new Switch?[] ProductList { get; set; }
//
//     public int Id
//     {
//         get => _id;
//         set
//         {
//             _id = value;
//             CurrentId = value + 1;
//             UpdateAllBarcodes();
//         }
//     }
//
//     public Switch? this[int index]
//     {
//         get
//         {
//             if (index < 0 || index >= ProductList.Length) return null;
//             return ProductList[index];
//         }
//         set
//         {
//             if (index < 0 || index >= ProductList.Length) return;
//             ChangeBarcodeText(value, index);
//             ProductList[index] = value;
//
//         }
//     }
//
//     protected SwitchWindow(int count) : base(count)
//     {
//         ProductList = new Switch?[count];
//         Id = CurrentId;
//         CurrentId += 1;
//     }
//
//     public static implicit operator SwitchWindow(int count)
//     {
//         return new SwitchWindow(count);
//     }
//
//     public static implicit operator SwitchWindow((int count, int id) tuple)
//     {
//         CurrentId = tuple.id;
//         return new SwitchWindow(tuple.count);
//     }
// }