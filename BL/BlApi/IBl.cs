﻿namespace BlApi;
/// <summary>
/// 
/// </summary>

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public void InitializeDB();
    public void ResetDB();

    public DateTime? StartDate{set; get; }

    public DateTime? EndDate { set; get; } 
    public DateTime Clock { get; set; }

}

