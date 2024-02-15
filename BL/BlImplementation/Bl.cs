
using BlApi;

namespace BlImplementation;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();

    public void InitializeDB() => DalTest.Initialization.Do();

    public void ResetDB()//TODO
    {
      DalTest.Initialization.Reset();
    }

}
