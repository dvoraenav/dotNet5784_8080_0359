
namespace Dal;
using DalApi;
using DO;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        foreach (Engineer eg in DataSource.Engineers)
            if (eg.Id == item.Id)//checking if the id of the giving engineer is already in the list
                throw new DalAlreadyExistsException($"Engineer with Id {item.Id} already exist");
        DataSource.Engineers.Add(item); //if not adding to the list of the engineers
        return item.Id;
    }

    public void Delete(int id) => DataSource.Engineers.Remove(
          DataSource.Engineers.Find(engineer => engineer.Id == id) ??
              throw new DalDoesNotExistException($"engineer with Id {id} does not exist"));


    public void Read(int id) => Read(x => x.Id == id);

    public Engineer? Read(Func<Engineer, bool> filter)=>DataSource.Engineers.Where(filter).FirstOrDefault();

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)=>
        filter != null
            ? from item in DataSource.Engineers
                   where filter(item)
                   select item
            : from item in DataSource.Engineers
               select item;
    public void Update(Engineer item)
    {
        Engineer e1 = DataSource.Engineers.Find(engineer => engineer.Id == item.Id)
            ?? throw new DalDoesNotExistException($"engineer with id {item.Id} does not exist");
        Delete(e1.Id);//deleting the old version
        Create(item);//creating a new virsion
    }
}
