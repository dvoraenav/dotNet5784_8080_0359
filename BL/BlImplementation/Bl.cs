
using BlApi;
using BO;

namespace BlImplementation;

internal class Bl : IBl
{

    DalApi.IDal dal = DalApi.Factory.Get;
    /// <summary>
    /// The start date of the project
    /// </summary>
    public DateTime? StartDate
    {
        get { return dal.StartDate; }
        set {if (StartDate == null)
                throw new Exception("A new project cannot be created. There is an existing project in progress");//TODO
             else dal.StartDate = value; }
    }

    /// <summary>
    /// the end date of the program
    /// </summary>
    public DateTime? EndDate
    {
        get { return dal.EndDate; }
        set
        {
            if (StartDate is null ||
                StartDate > value ||
                dal.Task.ReadAll(x => x.StartTask == null ||
                (x.StartTask + x.NumDays) > value).Any())
                throw new BlInvalidInputPropertyException("The Date is too early");//TODO
            dal.EndDate = value;
        }
    }

    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();

    public void InitializeDB() => DalTest.Initialization.Do();
    public void ResetDB() => DalTest.Initialization.Reset();
    private static DateTime s_Clock = DateTime.Now.Date;
    public DateTime Clock { get { return s_Clock; } private set { s_Clock = value; } }
    public void AdvanceTimeByYear() { Clock = Clock.AddYears(1); }
    public void AdvanceTimeByMounse() { Clock = Clock.AddSeconds(1); }
    public void AdvanceTimeByDay() { Clock = Clock.AddDays(1); }
    public void AdvanceTimeByHour() { Clock = Clock.AddHours(1); }
    public void ResetTime_() { Clock = DateTime.Now.Date; }


}
