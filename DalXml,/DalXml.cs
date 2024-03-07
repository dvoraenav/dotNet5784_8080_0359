using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;


sealed internal class DalXml : IDal
{
    public DateTime? StartDate
    {
        get { return Config.StartDate; }
        set { Config.StartDate = value; }

    }
    public DateTime? EndDate
    {
        get { return Config.EndDate; }
        set { Config.EndDate = value; }
    }

    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();

}
