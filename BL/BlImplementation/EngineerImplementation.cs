
namespace BlImplementation;
using BlApi;
using BO;
using System.Data.Common;
using System.Text.RegularExpressions;

internal class EngineerImplementation : IEngineer
{
    string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer? item)
    {
        //if (item.Id >= 0 && item.Name != "" && item.PayPerHour >= 0 && Regex.IsMatch(item.Mail, pattern)) { }        int enId=0;
        DO.Engineer _doengineer = new DO.Engineer(
             Id: item.Id,
             FullName: item.Name,
             Mail: item.Mail,
             PayPerHour: item.PayPerHour,
             Level: (DO.EngineerExpireance)item.Level);
        try
        {
            int enId = _dal.Engineer.Create(_doengineer);
            return enId;

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            //  throw new BO.BlAlreadyExistsException($"Engeineer with ID={boStudent.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer? Read(int id)
    {/////////////////////////////////////////////////////////////////////////////////////
        DO.Engineer? _doEngineer = _dal.Engineer.Read(x => x.Id == id);
        if (_doEngineer == null) { /*throw new NotImplementedException();*/ }

        return new BO.Engineer()
        {
            Id = _doEngineer.Id,
            Name = _doEngineer.FullName,
            Mail = _doEngineer.Mail,
            PayPerHour = _doEngineer.PayPerHour,
            Level = (BO.EngineerExpireance)_doEngineer.Level
        };
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    { throw new NotImplementedException(); }
    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }

}
