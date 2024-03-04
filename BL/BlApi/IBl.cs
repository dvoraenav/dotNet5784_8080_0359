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

    //DateTime Clock { get; set; }

}

