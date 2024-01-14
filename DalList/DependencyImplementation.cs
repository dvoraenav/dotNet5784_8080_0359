
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int newID = DataSource.Config.NextDependencyId;
        Dependency newItem = item with { Id = newID };
        DataSource.Dependencies.Add(newItem);
        return newID;

    }
    public void Delete(int id) => DataSource.Dependencies.Remove(
            DataSource.Dependencies.Find(dependency => dependency.Id == id) ??
                throw new DalDoesNotExistException($"dependancy with id {id} does not exist"));

    public void Read(int id) => Read(x => x.Id== id);
    public Dependency? Read(Func<Dependency, bool> filter) => DataSource.Dependencies.Where(filter).FirstOrDefault();
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) =>
        filter != null
            ? from item in DataSource.Dependencies
              where filter(item)
              select item
            : from item in DataSource.Dependencies
              select item;

    public void Update(Dependency item)
    {
        Dependency d1 = DataSource.Dependencies.Find(dependency => dependency.Id == item.Id) ??
            throw new DalDoesNotExistException($"dependancy with id {item.Id} does not exist");
        Delete(d1.Id); //deleting the old version
        Create(item); //creating a new vesion
    }
}
