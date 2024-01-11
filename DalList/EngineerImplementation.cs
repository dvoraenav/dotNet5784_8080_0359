
namespace Dal;
using DalApi;
using DO;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)//creating a new engineer
    {
        foreach (Engineer eg in DataSource.Engineers)
            if (eg.id == item.id)//checking if the id of the giving engineer is already in the list
                throw new Exception($"Engineer with ID={item.id} already exist");
        DataSource.Engineers.Add(item); //if not adding to the list of the engineers
        return item.id;
    }

    public void Delete(int id)
    {
        Engineer e1=DataSource.Engineers.Find(engineer => engineer.id == id);//looking for the Engineer
        if (e1 == null)
            throw new Exception($"engineer with id ={id} does not exist");
        DataSource.Engineers.Remove(e1);//removing from the list
    }

     public Engineer? Read(int id)
    {
       return DataSource.Engineers.Where(x=>x.id==id).FirstOrDefault();
    }

    public IEnumerable<Engineer> ReadAll()
    {
        return DataSource.Engineers.Select(x=>x);//list of Engineers
    }

    public void Update(Engineer item)
    {
        Engineer e1 = DataSource.Engineers.Find(engineer => engineer.id == item.id);//looking for the Engineer
        if (e1 == null)
            throw new Exception($"engineer with id ={item.id} does not exist");
        Delete(e1.id);//deleting the old version
        Create(item);//creating a new virsion
    }
}
