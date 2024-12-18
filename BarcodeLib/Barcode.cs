using System.Text;

namespace BarcodeLib;

public class Barcode : IBarcode
{
    // public Barcode()
    // {
    //     InitialString = "";
    // }

    public Barcode(string initialString)
    {
        InitialString = initialString;
    }

    private string _initialString;

    /// <summary>
    /// Setter refreshes Barcode on a change of InitialString
    /// </summary>
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
    
    public string BarcodeString { get; set; }
    public override string ToString()
    {
        return IBarcode.OutputType switch
        {
            BarcodeOutputType.Barcode => BarcodeString,
            BarcodeOutputType.Text => InitialString,
            BarcodeOutputType.Full => $"{BarcodeString}\n{InitialString,10}",
            _ => BarcodeString
        };
    }
}