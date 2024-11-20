namespace BarcodeLib;

public interface IBarcode
{
    public string InitialString { get; set; }
    public static BarcodeOutputType OutputType { get; set; } = BarcodeOutputType.Full;

}