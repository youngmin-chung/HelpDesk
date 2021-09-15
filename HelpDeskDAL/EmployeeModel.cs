using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace HelpDeskDAL
{
    public class EmployeeModel
    {
        IRepository<Employee> repo;
        public EmployeeModel()
        {
            repo = new HelpdeskRepository<Employee>();
        }

        //EmployeeModel.GetByEmail
        public Employee GetByEmail(string email)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(e => e.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }

        //EmployeeModel.GetByLastname
        public Employee GetByLastname(string name)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(e => e.LastName == name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }

        //EmployeeModel.GetById
        public Employee GetById(int id)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(e => e.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }
        //EmployeeModel.GetAll
        public List<Employee> GetAll()
        {
            List<Employee> allEmployees = new List<Employee>();

            try
            {
                allEmployees = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allEmployees;
        }

        //EmployeeModel.Add
        public int Add(Employee newEmployee)
        {
            try
            {
                newEmployee = repo.Add(newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return newEmployee.Id;
        }

        // EmployeeModel.Update
        public UpdateStatus Update(Employee updateEmployee)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                opStatus = repo.Update(updateEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return opStatus;
        }

        //EmployeeModel.Delete
        public int Delete(int id)
        {
            int employeesDeleted = -1;

            try
            {
                employeesDeleted = repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return employeesDeleted;
        }
    }
}
