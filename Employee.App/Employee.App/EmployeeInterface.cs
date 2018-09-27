using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.App
{

    interface IEmployee
    {
        int EmployeeId { get; set; }
        string Name { get; set; }
        string BaseLocation { get; set; }
    }
}
