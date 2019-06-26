using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{

    public class EmployeeImplementation : IEmployeeRepositary
    {
        private List<Employee> _emlist;

        public  EmployeeImplementation()
        {
            _emlist = new List<Employee>()
            {
                new Employee() { Id=1,Name="John",Department=Dept.HR},
                new Employee() { Id=2,Name="Hardin",Department=Dept.IT},
                new Employee() { Id=3,Name="Finch",Department=Dept.IT}
            };

        }

        public Employee Add(Employee emp)
        {
            emp.Id= _emlist.Max(i => i.Id + 1);
           _emlist.Add(emp);
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _emlist;
        }

        public Employee GetEmployee(int Id)
        {
            return _emlist.FirstOrDefault(e => e.Id == Id);
        }
       

        public Employee Delete(int id)
        {
            Employee empdel = _emlist.FirstOrDefault(e => e.Id == id);
            if (empdel != null)
                _emlist.Remove(empdel);

            return empdel;
        }
        
        public Employee Update(Employee emp)
        {
            Employee empupdate = _emlist.FirstOrDefault(e => e.Id == emp.Id);
            empupdate.Name = emp.Name;
            empupdate.Department = emp.Department;
            return empupdate;

        }
    }
}
