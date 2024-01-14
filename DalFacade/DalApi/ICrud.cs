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
        public void Read(int id); 
        public T? Read(Func<T, bool> filter);
        IEnumerable<T> ReadAll(Func<T, bool>? filter = null);
        void Update(T item);
        void Delete(int id);
    }

}
