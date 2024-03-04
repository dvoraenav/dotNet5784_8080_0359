
using DalApi;
using System.Xml.Linq;

namespace Dal;

internal class ClockImplemention : IClock
{
    private readonly string _clock_xml="data-config";
    public DateTime? GetEndProject()
    {
        throw new NotImplementedException();
    }

    public void resetTime()
    {
        throw new NotImplementedException();
    }

    public DateTime? SetEndProject(DateTime endProject)
    {
        throw new NotImplementedException();
    }

    public DateTime? SetStartProject(DateTime startProject)
    {
        throw new NotImplementedException();
    }
}

