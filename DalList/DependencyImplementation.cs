
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item) //creating a new Dependency
    {
        int newID = DataSource.Config.NextDependencyId; //new id number
        Dependency newItem = item with { id = newID };
        DataSource.Dependencies.Add(newItem);//adding to the list of Dependencys
        return newID;

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Dependency> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
