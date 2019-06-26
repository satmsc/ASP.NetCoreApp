using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class SqlEmpRepository : IEmployeeRepositary
    {
        private readonly AppDbContext context;

        public SqlEmpRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee emp)
        {
            context.employees.Add(emp);
            context.SaveChanges();
            return emp;
        }

        public Employee Delete(int id)
        {
            Employee em = context.employees.Find(id);
            if (em != null)
            {
                context.Remove(em);
                context.SaveChanges();
            }
            return em;

        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.employees;
        }

        public Employee GetEmployee(int Id)
        {
            Employee e= context.employees.Find(Id);
            return e;
        }

        public Employee Update(Employee emp)
        {
           var e=  context.employees.Attach(emp);
            e.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return emp;
        }
    }
}
