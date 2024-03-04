
using BlApi;

namespace BlImplementation;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();

    public IClock Clock => throw new NotImplementedException();

    public void InitializeDB() => DalTest.Initialization.Do();

    public void ResetDB()=> DalTest.Initialization.Reset();
    private static DateTime s_Clock = DateTime.Now.Date;
    public DateTime Clock_ { get { return s_Clock; }private set { s_Clock = value; } }
    public void AdvanceTimeByYear() { Clock_ = Clock_.AddYears(1); }
    public void AdvanceTimeByMounse() { Clock_ = Clock_.AddSeconds(1); }
    public void AdvanceTimeByDay() { Clock_ = Clock_.AddDays(1); }
    public void AdvanceTimeByHour() { Clock_ = Clock_.AddHours(1); }
    public void ResetTime_() { Clock_=DateTime.Now.Date; }
    

}
