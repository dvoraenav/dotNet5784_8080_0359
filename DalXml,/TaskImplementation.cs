
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";

    public int Create(Task item)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        int newID = tasks.Config.NextTaskID; //new id task number
        tasks.Add(item with { Id = newID });
        XMLTools.SaveListToXMLSerializer(s_tasks_xml);
        return newID;
    }

    public void Delete(int id)=>
          XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml).Remove(
          XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml).Find(task => task.Id == id) ??
              throw new DalDoesNotExistException($"task with Id {id} does not exist"));
        XMLTools.SaveListToXMLSerializer(s_tasks_xml);
   

    public void Read(int id)=> Read(x => x.Id == id);


    public Task? Read(Func<Task, bool> filter) => XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml).Where(filter).FirstOrDefault();

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) =>
         filter != null
            ? from item in XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml).Tasks
              where filter(item)
              select item
            : from item in XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml).Tasks
              select item;




    public void Update(Task item)
    {
        Task? t1 = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml).Find(task => task.Id == item.Id) ??
             throw new DalDoesNotExistException($"task with id {item.Id} does not exist");
        Delete(t1.Id); //deleting the old version
        Create(item);//creating a new virsiom
    }
}
