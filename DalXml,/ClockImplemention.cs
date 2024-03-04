
using DalApi;
using System.Xml.Linq;

namespace Dal;

internal class ClockImplemention : IClock
{
    private readonly string _clock_xml="data-config";
    public DateTime? GetEndProject()
    {
        XElement root = XMLTools.LoadListFromXMLElement(_clock_xml).Element("EndProject")!;//loading the value from Endproject tag into root
        if(root.Value=="")//there is no value
        {
            return null;
        }
        return DateTime.Parse(root.Value);
    }

    public DateTime? GetStartProject()
    {
        XElement root = XMLTools.LoadListFromXMLElement(_clock_xml).Element("StartProject")!;
        if (root.Value == "") 
        {
            return null;
        }
        return DateTime.Parse(root.Value);
    }

    public void resetTime()
    {
        XElement root = XMLTools.LoadListFromXMLElement(_clock_xml);
        root.Element("StartProject")!.Value = "";//put empty string
        root.Element("EndProject")!.Value = "";//put empty string
        XMLTools.SaveListToXMLElement(root, _clock_xml);//save changes to file
    }

    public DateTime? SetEndProject(DateTime endProject)
    {
        XElement root=XMLTools.LoadListFromXMLElement(_clock_xml);
        root.Element("EndProject")!.Value = endProject.ToString();
        XMLTools.SaveListToXMLElement(root, _clock_xml);
        return endProject;
    }

    public DateTime? SetStartProject(DateTime startProject)
    {
        XElement root = XMLTools.LoadListFromXMLElement(_clock_xml);
        root.Element("StartProject")!.Value = startProject.ToString();
        XMLTools.SaveListToXMLElement(root, _clock_xml);
        return startProject;
    }
}

