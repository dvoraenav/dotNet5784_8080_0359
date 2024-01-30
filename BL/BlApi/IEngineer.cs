using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// 
/// </summary>
    public interface IEngineer
    {
    /// <summary>
    ///  check the input and  try to add  the engineer to the data 
    ///  layer if the input is correct
    /// </summary>
    /// <param name="item"> object of engineer</param>
    /// <returns> the id</returns>
    public int AddEngineer(BO.Engineer item);
    /// <summary>
    /// build an engineer object with the data of the engineer and return it
    /// </summary>
    /// <param name="id">the identity of the engineer </param>
    /// <returns> engineer's object with the data want</returns>
    public BO.Engineer? Read(int id);
    /// <summary>
    /// ask for the engineer's list (with filter) and find the task for every engineer and 
    /// build a fit entity for every engineer
    /// </summary>
    /// <returns> the list was built</returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
    /// <summary>
    /// check the data and try to update
    /// </summary>
    /// <param name="item"> object of engineer that includes the update</param>
    public void Update(BO.Engineer item);
    /// <summary>
    /// check if the engineer is not in the middle of task and try to delete the engineer from dal
    /// we cant delete if he is in the middle of task or he finished the task
    /// throw errors if we cant delete or there is no engineer with this id
    /// </summary>
    /// <param name="id">the identity of the engineer</param>
    public void Delete(int id);
  

}

