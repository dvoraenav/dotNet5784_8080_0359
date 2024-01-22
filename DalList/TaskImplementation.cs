namespace Dal;
using DalApi;
using DO;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newID = DataSource.Config.NextTaskID; //new id task number
        DataSource.Tasks.Add(item with { Id = newID });
        return newID;
    }
    public void Delete(int id) => DataSource.Tasks.Remove(
          DataSource.Tasks.Find(task => task.Id == id) ??
              throw new DalDoesNotExistException($"task with Id {id} does not exist"));


    public void Read(int id) => Read(x => x.Id == id);
    public Task? Read(Func<Task, bool> filter) => DataSource.Tasks.Where(filter).FirstOrDefault();

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) =>
         filter != null
            ? from item in DataSource.Tasks
              where filter(item)
              select item
            : from item in DataSource.Tasks
              select item;


    public void Update(Task item)
    {
        Task? t1 = DataSource.Tasks.Find(task => task.Id == item.Id) ??
            throw new DalDoesNotExistException($"task with id {item.Id} does not exist");
        Delete(t1.Id); //deleting the old version
        Create(item);//creating a new virsiom
    }
    public void Clear() => DataSource.Tasks.Clear();
}

