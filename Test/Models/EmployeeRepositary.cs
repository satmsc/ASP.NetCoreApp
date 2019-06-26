using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public interface IEmployeeRepositary
    {
         Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployees();
        Employee Add(Employee emp);

        Employee Update(Employee emp);
        Employee Delete(int id);

    }
}
