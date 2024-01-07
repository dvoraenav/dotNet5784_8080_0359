
namespace DalApi;
using DO;
public interface ITask
{
    int Create(System.Threading.Tasks.Task item); //Creates new entity object in DAL
    System.Threading.Tasks.Task? Read(int id); //Reads entity object by its ID 
    List<System.Threading.Tasks.Task> ReadAll(); //stage 1 only, Reads all entity objects
    void Update(System.Threading.Tasks.Task item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id

}
