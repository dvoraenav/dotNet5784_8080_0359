
namespace BO;

public class EngineerInTask
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public override string ToString() => Tools.ToStringProperty(this);
}
