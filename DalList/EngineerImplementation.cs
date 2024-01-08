
namespace Dal;
using DalApi;
using DO;
using System.ComponentModel.Design;

public class EngineerImplementation : IEngineer
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
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
