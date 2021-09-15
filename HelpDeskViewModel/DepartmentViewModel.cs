using System;
using System.Collections.Generic;
using System.Reflection;
using HelpDeskDAL;


namespace HelpDeskViewModel
{
    public class DepartmentViewModel
    {
        private DepartmentModel _model;

        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Timer { get; set; }

        public DepartmentViewModel()
        {
            _model = new DepartmentModel();
        }

        //
        // Retrieve all the Departments
        //
        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allVms = new List<DepartmentViewModel>();
            try
            {
                List<Department> allDepartments = _model.GetAll();
                foreach (Department dep in allDepartments)
                {
                    DepartmentViewModel depVm = new DepartmentViewModel();
                    depVm.DepartmentName = dep.DepartmentName;
                    depVm.Id = dep.Id;
                    depVm.Timer = Convert.ToBase64String(dep.Timer);
                    allVms.Add(depVm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + ex.Message);
            }
            return allVms;
        }
    }
}
