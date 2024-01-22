namespace Dal;

using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static XMLTools;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";

    private const string _entityName = nameof(Dependency);
    private const string _id = nameof(Dependency.Id);
    private const string _currentTaskId = nameof(Dependency.CurrentTaskId);
    private const string _lastTaskId = nameof(Dependency.LastTaskId);


    /// <summary>
    /// creating a new dependency in XML file
    /// </summary>
    /// <param name="item">the dependency to add</param>
    /// <returns>retun the id of the new depndency</returns>
    public int Create(Dependency item)
    {
        XElement? dependencyList = LoadListFromXMLElement(s_dependencys_xml);
        var id = Config.NextDependencyId; //next atuo number of dependency
        dependencyList.Add(
            new XElement(_entityName,
            new XElement(_id, id),
            new XElement(_currentTaskId, item.CurrentTaskId),
            new XElement(_lastTaskId, item.LastTaskId))); //creating and adding
        SaveListToXMLElement(dependencyList, s_dependencys_xml);
        return id;
    }

    /// <summary>
    /// deleting an dependency from XML file
    /// </summary>
    /// <param name="id">the id of dependency we wish to delete</param>
    /// <exception cref="DalDoesNotExistException"> if the depedency do not exsit </exception>
    public void Delete(int id)
    {
        try
        {
            XElement? dependencyList = LoadListFromXMLElement(s_dependencys_xml);
            XElement? dep = (
                  from element in dependencyList.Elements()
                  where Convert.ToInt32(element.Element(_id)!.Value) == id
                  select element).FirstOrDefault(); //selecting the dependecy in the file with the same id
            if (dep is not null) dep.Remove();//deleting
            SaveListToXMLElement(dependencyList, s_dependencys_xml);
        }
        catch { throw new DalDoesNotExistException($"dependancy with id {id} does not exist"); }
    }

    /// <summary>
    /// an help metode- converting an Xelement object to Dependency
    /// </summary>
    /// <param name="d">the xelement object</param>
    /// <returns> the dependency object</returns>
    /// <exception cref="FormatException"> if cant convert </exception>
    static Dependency getDependency(XElement d)
    {
        return new Dependency()
        {
            Id = d.ToIntNullable(_id) ?? throw new FormatException("can't convert id"),
            CurrentTaskId = d.ToIntNullable(_currentTaskId) ?? throw new FormatException("can't convert current task id"),
            LastTaskId = d.ToIntNullable(_lastTaskId) ?? throw new FormatException("can't convert last task id")
        };
    }

    public void Read(int id) => Read(x => x.Id == id);
    public Dependency? Read(Func<Dependency, bool> filter) =>
            LoadListFromXMLElement(s_dependencys_xml).Elements().Select(x => getDependency(x)).FirstOrDefault(filter);

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) =>
          filter == null
           ? LoadListFromXMLElement(s_dependencys_xml).Elements().Select(x => getDependency(x))
           : LoadListFromXMLElement(s_dependencys_xml).Elements().Select(x => getDependency(x)).Where(filter);

    public void Update(Dependency item)
    { //DalDoesNotExistException ????
        XElement? dependencyList = LoadListFromXMLElement(s_dependencys_xml);
        if (Read(x => x.Id == item.Id) == null)
            throw new DalDoesNotExistException($"dependancy with id {item.Id} does not exist");
        Delete(item.Id);
        Create(item);
    }
    public void Clear()
    {
        XElement? dependencyList = LoadListFromXMLElement(s_dependencys_xml);
        dependencyList.RemoveAll();
        SaveListToXMLElement(dependencyList, s_dependencys_xml);
    }
}
