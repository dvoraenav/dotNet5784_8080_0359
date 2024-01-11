
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDoesNotExistException"> </exception>
    public void Delete(int id)
    {
        Dependency d1 = DataSource.Dependencies.Find(dependency => dependency.id == id);//looking for the Dependency
        if (d1==null)
            throw new DalDoesNotExistException($"dependancy with id {id} does not exist");
        DataSource.Dependencies.Remove(d1);//removing from the list
    }

    public void Read(int id)
    {
        Read(x => x.id == id);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns> </returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    { return DataSource.Dependencies.Where(filter).FirstOrDefault(); }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;
    }

    public void Update(Dependency item)
    {
        Dependency d1 = DataSource.Dependencies.Find(dependency => dependency.id == item.id);//looking for the Dependency
        if (d1 == null)
            throw new DalDoesNotExistException($"dependancy with id {item.id} does not exist");
        Delete(d1.id);//deleting the old version
        Create(item);//creating a new vesion
    }
}
