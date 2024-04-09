
namespace BO;

public class TaskInList
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }

    public override string ToString() => Tools.ToStringProperty(this);

}
