using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : class
    {
        /// <summary>
        /// Creates new entity object in DAL
        /// </summary>
        /// <param name="item">new object to add to the entity</param>
        /// <returns>the id of the new entity  (specially useful if it is an automatic number)</returns>
        int Create(T item);
        /// <summary>
        /// Return a reference to a single object with a specific id or null when the object does not exist
        /// </summary>
        /// <param name="id">Object ID number</param>
        public void Read(int id);
        /// <summary>
        /// search for the value that maintains the filter
        /// </summary>
        /// <param name="filter">A pointer to a filter boolean function</param>
        /// <returns>The first object in the list on which the function returns True
        public T? Read(Func<T, bool> filter);
        /// <summary>
        /// Return a copy of the list of references to all objects that meet the condition
        /// </summary>
        /// <param name="filter">A pointer to a filter boolean function </param>
        /// <returns>The list of all objects in the list for which the function returns True. 
        /// If no pointer is sent, the entire list will be returned </returns>
        IEnumerable<T> ReadAll(Func<T, bool>? filter = null);
        /// <summary>
        /// Update of an existing object
        /// </summary>
        /// <param name="item">A reference to an updated existing object</param>
        void Update(T item);
        /// <summary>
        /// Deleting an existing object with a certain ID, from the list of objects or
        ///Mark as "inactive".
        /// </summary>
        /// <param name="id">Object ID number</param>
        void Delete(int id);

        /// <summary>
        /// deleting the data from the xml file
        /// </summary>
        void Clear();
    }

}
