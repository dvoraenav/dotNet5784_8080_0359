namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public string? FullName { get; init; }
    public string? Mail { get; set; }
    public double Cost { get; set; }
    public EngineerExpireance Level { get; set; }
    public TaskInEngineer? Task { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}