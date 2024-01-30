
namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Mail { get; set; }
    public double PayPerHour { get; set; }
    public EngineerExpireance Level { get; set; }
    public  Tuple <int,string>? Task { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
