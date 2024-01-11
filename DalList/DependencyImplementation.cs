
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
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
        Dependency d1 = DataSource.Dependencies.Find(dependency => dependency.id == id);//looking for the Dependency
        if (d1==null)
            throw new Exception($"dependancy with id ={id} does not exist");
        DataSource.Dependencies.Remove(d1);//removing from the list
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Where(x => x.id == id).FirstOrDefault();
    }

     public IEnumerable<Dependency> ReadAll()
    {
        return DataSource.Dependencies.Select(x => x);//list of Dependencies
    }
    public void Update(Dependency item)
    {
        Dependency d1 = DataSource.Dependencies.Find(dependency => dependency.id == item.id);//looking for the Dependency
        if (d1 == null)
            throw new Exception($"dependancy with id ={item.id} does not exist");
        Delete(d1.id);//deleting the old version
        Create(item);//creating a new vesion
    }
}
