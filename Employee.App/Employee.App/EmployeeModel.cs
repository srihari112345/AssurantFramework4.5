using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.App
{
    class EmployeeModel : IEmployee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string BaseLocation { get; set; }
    }
}
