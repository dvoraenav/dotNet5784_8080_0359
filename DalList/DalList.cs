namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

sealed internal class DalList : IDal
{
    /// <summary>
    /// The start date of the project
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// the end date of the program
    /// </summary>
    private DateTime? endDate { get; set; }
    public DateTime? EndDate
    {
        get { return endDate; }
        set
        {
            if (StartDate == null || StartDate > value)//if there is no start date or the end date date is befor the start date
                throw new DalEarlyDatePropertyException("The end date of the project is not valid");
            endDate = value;

        }
    }

    public static IDal Instance { get; } = new DalList();//create new object
    private DalList() { }// private empty ctr
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public IClock Clock => throw new NotImplementedException();
}
