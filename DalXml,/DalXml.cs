using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;


sealed internal class DalXml : IDal
{
    public DateTime? StartDate { get; set; }

    private DateTime? endDate { get; set; }
    public DateTime? EndDate
    {
        get { return endDate; }
        set
        {
            if (StartDate == null || StartDate > value)
                throw new DalEarlyDatePropertyException("The end date of the project is not valid");
            endDate = value;

        }
    }
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();

    public IClock Clock => throw new NotImplementedException();
}
