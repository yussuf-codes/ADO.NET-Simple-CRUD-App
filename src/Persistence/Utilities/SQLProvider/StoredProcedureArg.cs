namespace Persistence.Utilities.SQLProvider;

public class StoredProcedureArg
{
    public required string Name { get; set; }
    public required object Value { get; set; }
}
