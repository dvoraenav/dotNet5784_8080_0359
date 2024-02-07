
namespace BO;

public class TaskInEngineer
{ 
    public int Id { get; init; }
    public string? Name { get; init; }
    public override string ToString() => Tools.ToStringProperty(this);
}
