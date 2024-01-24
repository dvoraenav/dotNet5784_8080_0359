using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Engineer
    {
        public int Id { get; init; }
        public string? Name { get; set; }
        public string? Mail { get; set; }
        public double PayPerHour { get; set; }
        EngineerExpireance Level = EngineerExpireance.Beginner;




  

    }
}
