
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using static XMLTools;

internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";

    public int Create(Task item)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        int newID = Config.NextTaskId; //new id task number
        tasks.Add(item with { Id = newID });
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        return newID;
    }
    public void Delete(int id)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        tasks.Remove(tasks.Find(task => task.Id == id) ??
        throw new DalDoesNotExistException($"Task with Id {id} does not exist"));
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }

    //public void Read(int id) => Read(x => x.Id == id);

    public Task? Read(Func<Task, bool> filter) => LoadListFromXMLSerializer<Task>(s_tasks_xml).Where(filter).FirstOrDefault();

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) =>
         filter != null
            ? from item in XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml)
              where filter(item)
              select item
            : from item in XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml)
              select item;

    public void Update(Task item)
    {
        List<Task> list = LoadListFromXMLSerializer<Task>(s_tasks_xml) ?? throw new DalDoesNotExistException($"task with id {item.Id} does not exist");

        int index = list.FindIndex(x => x.Id == item.Id);

        if (index == -1)
            throw new DalDoesNotExistException($"task with id {item.Id} does not exist");

        list[index] = item;
        SaveListToXMLSerializer<Task>(list, s_tasks_xml);
    }

    public void Clear()
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        tasks.Clear();
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);

    }
}
