﻿namespace Dal;
using DalApi;
using DO;

internal class TaskImplementation : ITask
{
    public int Create(Task item)//creating a new task
    {
        int newID = DataSource.Config.NextTaskID; //new id task number
        DataSource.Tasks.Add(item with { id = newID }); //adding to the list of tasks
        return newID;
    }

    public void Delete(int id)
    {
        Task t1 = DataSource.Tasks.Find(task => task.id == id);//looking for the task
        if (t1 == null)
            throw new Exception($"task with id ={id} does not exist");
        DataSource.Tasks.Remove(t1);//deleting from list
    }

    public Task? Read(int id)
    {
        Task t1 = DataSource.Tasks.Find(task => task.id == id);//looking for the task
        if (t1 == null)
            throw new Exception($"task with id ={id} does not exist");
        return t1; //returning the task
    }

    public List<Task> ReadAll()
    {
        return  new List<Task>(DataSource.Tasks);//list of all the tasks
    }

    public void Update(Task item)
    {
        Task t1 = DataSource.Tasks.Find(task => task.id == item.id);//looking for the task
        if (t1 == null)
            throw new Exception($"task with id ={item.id} does not exist");
        Delete(t1.id); //deleting the old version
        Create(item);//creating a new virsiom
    }
}

