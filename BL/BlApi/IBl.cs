namespace BlApi;
/// <summary>
/// 
/// </summary>

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public void InitializeDB();
    public void ResetDB();
    public void AdvanceTimeByYear();
    public void AdvanceTimeByMounse();
    public void AdvanceTimeByDay();
    public void AdvanceTimeByHour() ;
    public void ResetTime_();
    public DateTime? StartDate{set; get; }

    public DateTime? EndDate { set; get; } 
    public DateTime Clock { get; }

}

