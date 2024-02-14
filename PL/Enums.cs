using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;

    internal class ExpirieanceCollection : IEnumerable
    {
        static readonly IEnumerable<BO.EngineerExpireance> s_enums =
    (Enum.GetValues(typeof(BO.EngineerExpireance)) as IEnumerable<BO.EngineerExpireance>)!;

        public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
    }


