
namespace BO;

public class TaskInList
{
    public int Id { get; init; }
    public string? Name { get; init; }   
    public string? Description { get; init; }
    public BO.TaskStatus Status { get; init; }

    public override string ToString() => Tools.ToStringProperty(this);

}
