﻿
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
    /// <summary>
    /// creating a new object of entity
    /// </summary>
    /// <param name="item">the new BO object of entity</param>
    /// <returns>the id pg the object</returns>
    /// <exception cref="BlDoesNotExistException">if the task dosent exsit</exception>
    /// <exception cref="BlTaskAlreadyLinkToEngineerException"> the task is alrady linked to another engineer</exception>
    /// <exception cref="BO.BlAlreadyExistsException"> the engineer alresdy exsit</exception>
    public int Create(BO.Engineer item)
    {

        try
        {
            InputIntegrityCheck(item);                    //if everything is ok,we can try add
            DO.Engineer _doengineer = new DO.Engineer(   // create a new object with the item details
                                                         // to add to  data leyer(casting from bo to do)
                         Id: (int)item.Id,
                         FullName: item.FullName!,
                         Mail: item.Mail!,
                         Cost: item.Cost,
                         Level: (DO.EngineerExpireance)item.Level);
            LinkTaskToEngineer(item);
            return _dal.Engineer.Create(_doengineer);//add to data leyer

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engeineer with ID {item.Id} already exists", ex);
        }
        catch (BO.BlInvalidInputPropertyException ex)
        {
            throw new BlInvalidInputPropertyException(ex.Message);
        }
        catch (BO.BlGeneralExceptionException ex)
        {
            throw new BO.BlGeneralExceptionException(ex.Message);
        }

    }

    /// <summary>
    /// updating data of object
    /// </summary>
    /// <param name="item">The updated object</param>
    /// <exception cref="BO.BlDoesNotExistException"> the object to update does not exsit</exception>
    /// <exception cref="BO.BlInvalidInputPropertyException"> Invalid inpout</exception>
    /// <exception cref="BlDoesNotExistException">the task dose mot exsit</exception>
    /// <exception cref="BlTaskAlreadyLinkToEngineerException">the task is already linked to another engineer </exception>
    public void Update(BO.Engineer item)
    {
        DO.Engineer _doEngineer = _dal.Engineer.Read(x => x.Id == item.Id) ?? throw new BO.BlDoesNotExistException($"Engeineer with ID {item.Id} does not exists");//check if the engineer that i want to update,is exist in the data leyer
        InputIntegrityCheck(item);         //if everything is ok we can try to update
        if ((DO.EngineerExpireance)item.Level < _doEngineer.Level) { throw new BO.BlInvalidInputPropertyException("The new level of engineer too low"); }//we can make the level highest but not lower
        try
        {
            LinkTaskToEngineer(item);
            DO.Engineer updated_enginner = Tools.CopySimilarFields<BO.Engineer, DO.Engineer>(item);
            _dal.Engineer.Update(updated_enginner);      //update the engineer with his task
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException(ex.Message);
        }
        catch (BO.BlInvalidInputPropertyException ex)
        {
            throw new BlInvalidInputPropertyException(ex.Message);
        }
        catch (BO.BlGeneralExceptionException ex)
        {
            throw new BO.BlGeneralExceptionException(ex.Message);
        }
    }

    /// <summary>
    ///  Deleting an entity object
    /// </summary>
    /// <param name="id">the id of the object to delete </param>
    /// <exception cref="BlCantBeEraseException">the object can not be delete </exception>
    /// <exception cref="BO.BlDoesNotExistException">the obect does not exsit</exception>
    public void Delete(int id)
    {
        if (_dal.Task.ReadAll(task => task.EngineerId == id && (Tools.SetStatus(task) is TaskStatus.Done or TaskStatus.OnTrack)).Any())//if the engineer with this id is exist but there is any task thet he finished or in the middle of-so we can't delete him
        {
            throw new BlCantBeEraseException($"Engeineer can not be erased");
        }
        try
        {
            _dal.Engineer.Delete(id);// try to delete the engineer with this id
        }

        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engeineer with ID {id} dose not exists", ex);
        }
    }

    /// <summary>
    /// Returning an entity object
    /// </summary>
    /// <param name="id">the id of the object to return</param>
    /// <returns>the object </returns>
    /// <exception cref="BO.BlDoesNotExistException"> if the object dose not exsit</exception>
    public BO.Engineer Read(int id)
    {
        DO.Engineer _doEngineer = _dal.Engineer.Read(x => x.Id == id) ?? throw new BO.BlDoesNotExistException($"Engeineer with ID {id} does not exists");//check if engineer with this id is exist

        return new BO.Engineer() //build new entity of engineer  and return it
        {
            Id = _doEngineer.Id,
            FullName = _doEngineer.FullName,
            Mail = _doEngineer.Mail,
            Cost = _doEngineer.Cost,
            Level = (BO.EngineerExpireance)_doEngineer.Level,
            Task = GetTaskInEngineer(id)// culculate the current task
        };
    }


    /// <summary>
    /// retuning a list of the entity objects
    /// </summary>
    /// <param name="filter">Filters which objects to return</param>
    /// <returns> list of the entity objects by filter if exsit if not return all </returns>
    public IEnumerable<BO.Engineer> GetEngineerList(Func<BO.Engineer, bool>? filter = null)
    {

        return (from engineer in _dal.Engineer.ReadAll()//going into the engineers list and build a new entitiy for everyone
                select new BO.Engineer()
                {
                    Id = engineer.Id,
                    FullName = engineer.FullName,
                    Mail = engineer.Mail,
                    Cost = engineer.Cost,
                    Level = (BO.EngineerExpireance)engineer.Level,
                    Task = GetTaskInEngineer(engineer.Id)
                }).Where(engineer => filter is null ? true : filter(engineer));
    }

    /// <summary>
    /// Checking if the object field values ​​are valid
    /// </summary>
    /// <param name="item">the object </param>
    /// <exception cref="BO.BlNegtivePropertyException"> if the id or cost is negtive </exception>
    /// <exception cref="BO.BlNullPropertyException">if the name is an empty string</exception>
    /// <exception cref="BO.BlInvalidInputPropertyException">if the email is Invalid </exception>
    private static void InputIntegrityCheck(BO.Engineer? item)
    {
        if (!IsAllDigits(item.Id.ToString()))
            throw new BO.BlInvalidInputPropertyException($"Engeineer's Id can only contains numbers");
        if (item!.Id <= 0)
            throw new BO.BlInvalidInputPropertyException($"Engeineer's Id can not be negative");
        if (item.FullName == "" || item.FullName == null)
            throw new BO.BlInvalidInputPropertyException($"Engeineer's name can not be empty");
        if (!IsAllLetters(item.FullName))
            throw new BO.BlInvalidInputPropertyException($"Engeineer's name can only contains letters");
        if (item.Cost <= 0 )
            throw new BO.BlInvalidInputPropertyException($"Engeineer's cost can not be negative or 0");
        if (!IsAllDigits(item.Cost.ToString()))
            throw new BO.BlInvalidInputPropertyException($"Engeineer's cost can only contains numbers");
        if (!new EmailAddressAttribute().IsValid(item.Mail))// only return true if there is only 1 '@' character
            // and it is neither the first nor the last character
            throw new BO.BlInvalidInputPropertyException($"Engeineer's mail address is not valid");
    }
    /// <summary>
    /// creating an entity object for task of enginneer
    /// </summary>
    /// <param name="id">the id of the task</param>
    /// <returns>an entity object </returns>
    public TaskInEngineer? GetTaskInEngineer(int id)
    {
        return _dal.Task.Read(task => task.EngineerId == id && 
        (Tools.SetStatus(task) is TaskStatus.OnTrack||
        Tools.SetStatus(task) is TaskStatus.Scheduled)) is DO.Task task ?
                    new TaskInEngineer()
                    {
                        Id = task.Id,
                        Name = task.Name,
                    } : null;
    }

    private void LinkTaskToEngineer (BO.Engineer item)
    {
        if (item.Task is not null && item.Task.Id != 0) //If a task has been assigned to an engineer and the id of the task is not 0
        {
            DO.Task task = _dal.Task.Read(x => x.Id == item.Task!.Id) ?? throw new BlDoesNotExistException($"Task with Id {item.Task!.Id} does not exist");//look for the task in the data leyer
            if (task.EngineerId is not null && task.EngineerId != item.Id)// If there is an engineer to whom this task has been assigned, and his ID is
                throw new BlGeneralExceptionException($"Task is already link to an engineer");

            task = task with { EngineerId = item.Id };                //Now this task is assigned to the new engineer
            _dal.Task.Update(task);                                  //Updated the data layer in the task assigned to the engineer
        }
    }
}
