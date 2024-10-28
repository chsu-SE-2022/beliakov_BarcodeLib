using System.Text;

namespace BarcodeLib;

public static class BarcodeCreator
{
    private const int Height = 10;

    private const string Frame = "0000";

    private static readonly char[] Bars = { '█', '▌', '▐', ' ' };

    // Changes code type?
    private static void ChangeCodeType(string str, StringBuilder strBuilder, IList<int> codeList, ref bool
        isNumeric, ref int substrStartIndex)
    {
        if ((isNumeric && !IsNumeric(str, substrStartIndex, 2)) ||
            (!isNumeric && IsNumeric(str, substrStartIndex, 4)))
        {
            isNumeric = !isNumeric;
            AddPatternToCode(strBuilder, codeList, isNumeric ? SwitchToNumbers : SwitchToText);
        }
        MakeStep(ref substrStartIndex, codeList, isNumeric, str, strBuilder);
    }

    /// <summary>
    /// Make a step
    /// </summary>
    /// <param name="strIndex"></param>
    /// <param name="codeList"></param>
    /// <param name="isNumeric"></param>
    /// <param name="substr"></param>
    /// <param name="strBuilder"></param>
    private static void MakeStep(ref int strIndex, IList<int> codeList, bool isNumeric, string substr,
        StringBuilder strBuilder)
    {
        if (isNumeric)
        {
            AddPatternToCode(strBuilder, codeList, Array.IndexOf(NumberSymbols, substr.Substring(strIndex, 2)));
            strIndex += 2;
        }
        else
        {
            AddPatternToCode(strBuilder, codeList, Array.IndexOf(TextSymbols, substr.Substring(strIndex, 1)));
            strIndex++;
        }
    }

    /// <summary>
    /// Adds pattern to code
    /// </summary>
    /// <param name="codeBuilder"></param>
    /// <param name="codeList"></param>
    /// <param name="code"></param>
    private static void AddPatternToCode(StringBuilder codeBuilder, IList<int> codeList, int code)
    {
        codeBuilder.Append(Patterns[code]);
        codeList.Add(code);
    }

    /// <summary>
    /// Checks if the entire substring consists of digits
    /// </summary>
    /// <param name="str">Initial string</param>
    /// <param name="startIndex">Index of the first taken char</param>
    /// <param name="substringLength">Length of the substring</param>
    /// <returns></returns>
    private static bool IsNumeric(string str, int startIndex, int substringLength)
    {
        var chars = str.Skip(startIndex).Take(substringLength);
        return chars.Count() == substringLength && chars.All(x => char.IsDigit(x));
    }
    
    /// <summary>
    /// Calculate a code list's checksum
    /// </summary>
    /// <param name="codeList"></param>
    /// <returns></returns>
    private static int CalculateChecksum(IList<int> codeList)
    {
        var sum = codeList[0];
        for (var i = 1; i < codeList.Count; i++)
        {
            sum += i * codeList[i];
        }
        sum %= 103;
        return sum;
    }

    /// <summary>
    /// Convert a pattern (11-char long string of 1s and 0s) to a unicode bar
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    private static char PatternToBar(string pattern) => Bars[Convert.ToInt32(pattern, 2)];

    /// <summary>
    /// Splits <paramref name="str"/> into parts of <paramref name="partLength"/> length
    /// </summary>
    /// <param name="str">String to split</param>
    /// <param name="partLength">Length of each part</param>
    /// <returns></returns>
    private static IEnumerable<string> SplitString(this string str, int partLength)
    {
        return Enumerable
            .Range(0, str.Length / partLength)
            .Select(i => str.Substring(i * partLength, partLength));
    }

    public static string GetCode(string text)
    {
        var stringBuilder = new StringBuilder(Frame + Patterns[StartText]);
        var codeList = new List<int> { StartText };

        foreach (var v in text)
        {
            var patternIndex = Array.IndexOf(TextSymbols, v.ToString());
            stringBuilder.Append(Patterns[patternIndex]);
            codeList.Add(patternIndex);
        }
        int checksum = CalculateChecksum(codeList);
        stringBuilder.Append(Patterns[checksum]);
        stringBuilder.Append(Patterns[Stop]);
        stringBuilder.Append(Frame);

        var barcodeBuilder = new StringBuilder();
        foreach (var code in stringBuilder.ToString().SplitString(2))
        {
            barcodeBuilder.Append(PatternToBar(code));
        }

        var barcodeLength = barcodeBuilder.Length + 6;
        var frameBar = PatternToBar(Frame).ToString();
        var barcode = new StringBuilder();
        for (var i = 0; i < barcodeLength - 2; i++)
        {
            barcode.Append(frameBar);
        }
        barcode.Append("\n");

        for (var i = 0; i < Height; i++)
        {
            barcode.Append(frameBar + frameBar);
            barcode.Append(barcodeBuilder.ToString());
            barcode.Append(frameBar + frameBar + "\n");

        }
        for (var i = 0; i < barcodeLength - 2; i++)
        {
            barcode.Append(frameBar);
        }
        return barcode.ToString();

    }
    /// <summary>
    /// Initial pattern for a text string
    /// </summary>
    private const int StartText = 104;
    /// <summary>
    /// Initial pattern for a number string
    /// </summary>
    private const int StartNumbers = 105;
    /// <summary>
    /// Pattern for a switch to number (en/de)coding
    /// </summary>
    private const int SwitchToNumbers = 99;
    /// <summary>
    /// Pattern for a switch to text (en/de)coding
    /// </summary>
    private const int SwitchToText = 100;
    /// <summary>
    /// Pattern for stopping the (en/de)coding
    /// </summary>
    private const int Stop = 108;
    /// <summary>
    /// Allowed patterns
    /// </summary>
    private static readonly string[] Patterns = {
        "11011001100", "11001101100", "11001100110", "10010011000", "10010001100",
        "10001001100", "10011001000", "10011000100", "10001100100", "11001001000",
        "11001000100", "11000100100", "10110011100", "10011011100", "10011001110",
        "10111001100", "10011101100", "10011100110", "11001110010", "11001011100",
        "11001001110", "11011100100", "11001110100", "11101101110", "11101001100",
        "11100101100", "11100100110", "11101100100", "11100110100", "11100110010",
        "11011011000", "11011000110", "11000110110", "10100011000", "10001011000",
        "10001000110", "10110001000", "10001101000", "10001100010", "11010001000",
        "11000101000", "11000100010", "10110111000", "10110001110", "10001101110",
        "10111011000", "10111000110", "10001110110", "11101110110", "11010001110",
        "11000101110", "11011101000", "11011100010", "11011101110", "11101011000",
        "11101000110", "11100010110", "11101101000", "11101100010", "11100011010",
        "11101111010", "11001000010", "11110001010", "10100110000", "10100001100",
        "10010110000", "10010000110", "10000101100", "10000100110", "10110010000",
        "10110000100", "10011010000", "10011000010", "10000110100", "10000110010",
        "11000010010", "11001010000", "11110111010", "11000010100", "10001111010",
        "10100111100", "10010111100", "10010011110", "10111100100", "10011110100",
        "10011110010", "11110100100", "11110010100", "11110010010", "11011011110",
        "11011110110", "11110110110", "10101111000", "10100011110", "10001011110",
        "10111101000", "10111100010", "11110101000", "11110100010", "10111011110",
        // 100+
        "10111101110", "11101011110", "11110101110", "11010000100", "11010010000",
        "11010011100", "11000111010", "11010111000", "1100011101011"};
    /// <summary>
    /// Allowed characters
    /// </summary>
    private static readonly string[] TextSymbols = {
        " ","!","\"","#","$","%","&","'","(",")",
        "*","+",",","-",".","/","0","1","2","3",
        "4","5","6","7","8","9",":",";","<","=",
        ">","?","@","A","B","C","D","E","F","G",
        "H","I","J","K","L","M","N","O","P","Q",
        "R","S","T","U","V","W","X","Y","Z","[",
        "\\","]","^","_","`","a","b","c","d","e",
        "f","g","h","i","j","k","l","m","n","o",
        "p","q","r","s","t","u","v","w","x","y",
        "z","{","|","|","~"
    };

    /// <summary>
    /// Allowed digit pairs
    /// </summary>
    private static readonly string[] NumberSymbols = {
        "00","01","02","03","04","05","06","07","08","09",
        "10","11","12","13","14","15","16","17","18","19",
        "20","21","22","23","24","25","26","27","28","29",
        "30","31","32","33","34","35","36","37","38","39",
        "40","41","42","43","44","45","46","47","48","49",
        "50","51","52","53","54","55","56","57","58","59",
        "60","61","62","63","64","65","66","67","68","69",
        "70","71","72","73","74","75","76","77","78","79",
        "80","81","82","83","84","85","86","87","88","89",
        "90","91","92","93","94","95","96","97","98","99"
    };
}