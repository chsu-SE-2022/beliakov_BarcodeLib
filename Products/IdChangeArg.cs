namespace Products;

public sealed class IdChangeArg : EventArgs
{
    public int? OldId { get; init; }
    public int? NewId { get; init; }

}