namespace DalApi
{
    public interface IDal
    {
        DateTime? StartDate { get; set; }

        DateTime? EndDate { get; set; }
       
        IEngineer Engineer { get; }
        ITask Task { get; }
        IDependency Dependency { get; }
        IClock Clock { get; }

    }
}
