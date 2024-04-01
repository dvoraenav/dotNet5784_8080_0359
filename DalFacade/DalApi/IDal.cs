namespace DalApi
{
    public interface IDal
    {
        DateTime? StartDate { get; set; }

        DateTime? EndDate { get; set; }

        DateTime Clock { set; get; } 

        IEngineer Engineer { get; }
        ITask Task { get; }
        IDependency Dependency { get; }

    }
}
