using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HelpDeskDAL
{
    public class DepartmentModel
    {
        private IRepository<Department> repo;
        public DepartmentModel()
        {
            repo = new HelpdeskRepository<Department>();
        }

        // Retrieve all the Department
        public List<Department> GetAll()
        {
            List<Department> allDepartments = new List<Department>();

            try
            {
                allDepartments = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allDepartments;
        }

        public int Add(Department newDepartment)
        {
            try
            {
                newDepartment = repo.Add(newDepartment);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return newDepartment.Id;
        }

        public int Delete(int id)
        {
            int departmentDeleted = -1;

            try
            {
                departmentDeleted = repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return departmentDeleted;
        }

        public Department GetById(int id)
        {
            List<Department> selectedDepartment = null;

            try
            {
                selectedDepartment = repo.GetByExpression(dep => dep.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedDepartment.FirstOrDefault();
        }

        public Department GetByDepartmentName(string name)
        {
            List<Department> selectedDepartment = null;

            try
            {
                selectedDepartment = repo.GetByExpression(dep => dep.DepartmentName == name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedDepartment.FirstOrDefault();
        }

        public UpdateStatus Update(Department updateDepartment)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                opStatus = repo.Update(updateDepartment);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return opStatus;
        }
    }
}
