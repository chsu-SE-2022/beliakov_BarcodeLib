namespace Products;

public sealed class IdChangeArg : EventArgs
{
    public int? OldId { get; set; }
    public int? NewId { get; set; }

}