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
    private const string _lastTaskId = nameof(Dependency.Id);


    /// <summary>
    /// creating a new dependency and adding to the file
    /// </summary>
    /// <param name="item">the dependency to add</param>
    /// <param name="id">the atuo number of the dependency </param>
    private void helpAddDependency(Dependency item, int? id)
    {
        XElement? dependencyList = LoadListFromXMLElement(s_dependencys_xml);

        dependencyList.Add(new XElement(_entityName, new XElement(_id, id ?? item.Id),
            new XElement(_currentTaskId, item.CurrentTaskId),
            new XElement(_lastTaskId, item.LastTaskId))); //creating and adding

        SaveListToXMLElement(dependencyList, s_dependencys_xml);
    }

    /// <summary>
    /// retuning dependency that matches the filter
    /// </summary>
    /// <param name="filter"> the filter </param>
    /// <returns> the item that matches the filter</returns>
    private IEnumerable<Dependency> helpGetDependency(Func<Dependency, bool> filter = null!) =>
         from element in LoadListFromXMLElement(s_dependencys_xml).Elements()
         let isFilterNull = filter is null //if there is no filter
         let dependency = new Dependency //conveting XElement Dependency
         {
             Id = int.Parse(element.Element(_id)!.Value),
             CurrentTaskId = int.Parse(element.Element(_currentTaskId)!.Value),
             LastTaskId = int.Parse(element.Element(_lastTaskId)!.Value)
         }
         where isFilterNull ? isFilterNull : filter(dependency) //if there is filter
         select dependency;

    public int Create(Dependency item)
    {
        XElement? dependencyList = LoadListFromXMLElement(s_dependencys_xml);

        var id = Config.NextDependencyId; //next atuo number of dependency

        helpAddDependency(item, id);

        return id;
    }



    public void Delete(int id)
    {
        try
        {
            XElement? dependencyList = LoadListFromXMLElement(s_dependencys_xml);
            Dependency? dep = helpGetDependency(x => x.Id == id).FirstOrDefault();
            //XElement? dep = (
            //      from element in dependencyList.Elements()
            //      where Convert.ToInt32(element.Element(_id)!.Value) == id //if there is filter
            //      select element).FirstOrDefault();
            //dep.Remove();
        }
        catch { throw new DalDoesNotExistException($"dependancy with id {id} does not exist"); }
    }

    public void Read(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
