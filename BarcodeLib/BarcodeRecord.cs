namespace BarcodeLib;

public record class BarcodeRecord : IBarcode
{
    public BarcodeRecord(string initialString)
    {
        InitialString = initialString;
    }
    private string _initialString;
    public string InitialString
    {
        get => _initialString;
        set
        {
            if (value == _initialString) { return; }
            _initialString = value;
            BarcodeString = BarcodeCreator.GetCode(value);
        }
    }
    public string BarcodeString { get; set; }

    public override string ToString()
    {
        return IBarcode.OutputType switch
        {
            BarcodeOutputType.Barcode => BarcodeString,
            BarcodeOutputType.Text => $"* {InitialString} *",
            BarcodeOutputType.Full => $"{BarcodeString},\n*{InitialString,10}*\n",
            _ => BarcodeString
        };
    }
};