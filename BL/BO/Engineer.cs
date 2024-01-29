
namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Mail { get; set; }
    public double PayPerHour { get; set; }
    EngineerExpireance Level = EngineerExpireance.Beginner;
    public  BO.TaskInEngineer? Task { get; set; }
    public override string ToString() => this.ToStringProperty();
}
