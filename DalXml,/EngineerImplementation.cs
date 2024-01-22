
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineers_xml = "engineers";

    public int Create(Engineer item)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        foreach (Engineer eg in engineers)
            if (eg.Id == item.Id)//checking if the id of the giving engineer is already in the list
                throw new DalAlreadyExistsException($"Engineer with Id {item.Id} already exist");
        engineers.Add(item); //if not adding to the list of the engineers
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        engineers.Remove(engineers.Find(engineer => engineer.Id == id) ??
               throw new DalDoesNotExistException($"engineer with Id {id} does not exist"));
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);

    }

    public void Read(int id) => Read(x => x.Id == id);


    public Engineer? Read(Func<Engineer, bool> filter) =>
         XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml).Where(filter).FirstOrDefault();


    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) =>
        filter != null
            ? from item in XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml)
              where filter(item)
              select item
            : from item in XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml)
              select item;


    public void Update(Engineer item)
    {

        Engineer e1 = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml).Find(engineer => engineer.Id == item.Id)
            ?? throw new DalDoesNotExistException($"engineer with id {item.Id} does not exist");
        Delete(e1.Id);//deleting the old version
        Create(item);//creating a new virsion
    }
    public void Clear()
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        engineers.Clear();
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
    }
}
