namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

sealed internal class DalList : IDal
{
    /// <summary>
    /// The start date of the project
    /// </summary>
    public DateTime? StartDate
    {
        get { return DataSource.Config.StartDate; }
        set { DataSource.Config.StartDate = value; }
    }

    /// <summary>
    /// the end date of the program
    /// </summary>
    public DateTime? EndDate
    {
        get { return DataSource.Config.EndDate; }
        set { DataSource.Config.EndDate = value; }
    }

    public static IDal Instance { get; } = new DalList();//create new object
    private DalList() { }// private empty ctr
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

   
}
