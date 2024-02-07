
namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static BO.Tools;
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer? item)
    {

        try
        {
            InputIntegrityCheck(item);
            DO.Engineer _doengineer = new DO.Engineer(
                         Id: item.Id,
                         FullName: item.FullName,
                         Mail: item.Mail!,
                         PayPerHour: item.PayPerHour,
                         Level: (DO.EngineerExpireance)item.Level);

            if (item.Task is not null && item.Task.Id != 0)
            {
                DO.Task task = _dal.Task.Read(x => x.Id == item.Task!.Id) ?? throw new BlDoesNotExistException($"Task with Id {item.Task!.Id} does not exist"); ;

                if (task.EngineerId is not null && task.EngineerId != item.Id)
                    throw new BlTaskAlreadyLinkToEngineerException($"Task is already link to an engineer");

                task = task with { EngineerId = item.Id };
                _dal.Task.Update(task);
            }

            return _dal.Engineer.Create(_doengineer);

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engeineer with ID {item.Id} already exists", ex);
        }

    }
    public void Update(BO.Engineer item)
    {
        DO.Engineer _doEngineer = _dal.Engineer.Read(x => x.Id == item.Id) ?? throw new BO.BlDoesNotExistException($"Engeineer with ID {item.Id} does not exists");
        InputIntegrityCheck(item);
        if ((DO.EngineerExpireance)item.Level < _doEngineer.Level) { throw new BO.BlInvalidInputPropertyException("The new level of engineer too low"); }
        try
        {
            if (item.Task is not null)
            {
                DO.Task task = _dal.Task.Read(x => x.Id == item.Task!.Id) ?? throw new BlDoesNotExistException($"Task with Id {item.Task!.Id} does not exist"); ;

                if (task.EngineerId is not null && task.EngineerId != item.Id)
                    throw new BlTaskAlreadyLinkToEngineerException($"Task is already link to an engineer");

                task = task with { EngineerId = item.Id };
                _dal.Task.Update(task);
            }

            _dal.Engineer.Update(_doEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engeineer with ID {item.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        if (_dal.Task.ReadAll(task => task.EngineerId == id && (Tools.SetStatus(task) is TaskStatus.Done or TaskStatus.OnTrack)).Any())
        {
            throw new BlCantBeEraseException($"Engeineer can not be erased");
        }
        try
        {
            _dal.Engineer.Delete(id);
        }

        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engeineer with ID {id} dose not exists", ex);
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer _doEngineer = _dal.Engineer.Read(x => x.Id == id) ?? throw new BO.BlDoesNotExistException($"Engeineer with ID {id} does not exists");

        return new BO.Engineer()
        {
            Id = _doEngineer.Id,
            FullName = _doEngineer.FullName,
            Mail = _doEngineer.Mail,
            PayPerHour = _doEngineer.PayPerHour,
            Level = (BO.EngineerExpireance)_doEngineer.Level,
            Task = getTaskInEngineer(id)
        };
    }

    public IEnumerable<BO.Engineer> GetEngineerList(Func<BO.Engineer, bool> filter = null)
    {

        return (from engineer in _dal.Engineer.ReadAll()
                select new BO.Engineer()
                {
                    Id = engineer.Id,
                    FullName = engineer.FullName,
                    Mail = engineer.Mail,
                    PayPerHour = engineer.PayPerHour,
                    Level = (BO.EngineerExpireance)engineer.Level,
                    Task = getTaskInEngineer(engineer.Id)
                }).Where(engineer => filter is null ? true : filter(engineer));
    }

    private void InputIntegrityCheck(BO.Engineer? item)
    {
        if (item.Id <= 0)
            throw new BO.BlNegtivePropertyException($"Engeineer's Id can not be negative");
        if (item.FullName == "")
            throw new BO.BlNullPropertyException($"Engeineer's name can not be negative");
        if (item.PayPerHour <= 0)
            throw new BO.BlNegtivePropertyException($"Engeineer's cost can not be negative");
        if (!new EmailAddressAttribute().IsValid(item.Mail))
            throw new BO.BlInvalidInputPropertyException($"Engeineer's mail address is not valid");
    }

    private TaskInEngineer? getTaskInEngineer(int id)
    {
        return _dal.Task.Read(task => task.EngineerId == id && Tools.SetStatus(task) is TaskStatus.OnTrack) is DO.Task task ?
                    new TaskInEngineer()
                    {
                        Id = task.Id,
                        Name = task.Name,
                    } : null;
    }
    //return _dal.Engineer.ReadAll().Select(engineer =>
    //       {
    //       var boEngineer = engineer.CopySimilarFields<DO.Engineer, Engineer>();
    //       boEngineer.Task = getTaskInEngineer(boEngineer.Id);
    //       return boEngineer;
    //   }).Where(engineer => filter is null ? true : filter(engineer));
}
