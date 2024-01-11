
namespace Dal;
using DalApi;
using DO;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)//creating a new engineer
    {
        foreach (Engineer eg in DataSource.Engineers)
            if (eg.id == item.id)//checking if the id of the giving engineer is already in the list
                throw new DalAlreadyExistsException($"Engineer with ID{item.id} already exist");
        DataSource.Engineers.Add(item); //if not adding to the list of the engineers
        return item.id;
    }

    public void Delete(int id)
    {
        Engineer e1=DataSource.Engineers.Find(engineer => engineer.id == id);//looking for the Engineer
        if (e1 == null)
            throw new DalDoesNotExistException($"engineer with id {id} does not exist");
        DataSource.Engineers.Remove(e1);//removing from the list
    }

    public void Read(int id)
    {
        Read(x => x.id == id);
    }
    public Engineer? Read(Func<Engineer, bool> filter)
    { return DataSource.Engineers.Where(filter).FirstOrDefault(); }

    public IEnumerable<Engineer> ReadAll(Func<Engineer , bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }


    public void Update(Engineer item)
    {
        Engineer e1 = DataSource.Engineers.Find(engineer => engineer.id == item.id);//looking for the Engineer
        if (e1 == null)
            throw new DalDoesNotExistException($"engineer with id {item.id} does not exist");
        Delete(e1.id);//deleting the old version
        Create(item);//creating a new virsion
    }
}
