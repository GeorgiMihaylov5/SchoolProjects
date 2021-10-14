using CompanyDB.Data;
using CompanyDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDB.Services
{
    public class EmployeeServices
    {
        private CompanyContext companyContext;

        public List<Employee> GetAll()
        {
            using (companyContext = new CompanyContext())
            {
                return companyContext.Employees.ToList();
            }
        }
        public Employee Get(int id)
        {
            using (companyContext = new CompanyContext())
            {
                var employee = companyContext.Employees.Find(id);
                return employee;

            }
        }
        public void Add(Employee employee)
        {
            using (companyContext = new CompanyContext())
            {
                companyContext.Employees.Add(employee);
                companyContext.SaveChanges();
            }
        }
        public void Update(Employee employee)
        {
            using (companyContext = new CompanyContext())
            {
                var oldEmployee = companyContext.Employees.Find(employee.Id);

                if (oldEmployee != null)
                {
                    companyContext.Entry(oldEmployee).CurrentValues.SetValues(employee);

                    companyContext.SaveChanges();
                }
            }
        }
        public void Delete(int id)
        {
            using (companyContext = new CompanyContext())
            {
                var employee = companyContext.Employees.Find(id);
                if (employee != null)
                {
                    companyContext.Employees.Remove(employee);
                    companyContext.SaveChanges();
                }
            }

        }
    }
}
