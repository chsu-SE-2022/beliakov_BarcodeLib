using System.Text;

namespace BarcodeLib;

public class Barcode
{

    public Barcode(string initialString)
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
            this._initialString = value;
            this.BarcodeString = BarcodeCreator.GetCode(value);
        }
    }
    
    public string BarcodeString { get; private set; }
    public static BarcodeOutputType OutputType { get; set; }

    public override string ToString()
    {
        return OutputType switch
        {
            BarcodeOutputType.Barcode => BarcodeString,
            BarcodeOutputType.Text => InitialString,
            BarcodeOutputType.Full => $"{BarcodeString}\n{InitialString,10}",
            _ => BarcodeString
        };
    }
}