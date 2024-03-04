using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IClock
    {
        public DateTime? SetStartProject(DateTime startProject);
        public DateTime? SetEndProject(DateTime endProject);
        public DateTime? GetEndProject();
        public DateTime? GetStartProject();
        
    }
}
