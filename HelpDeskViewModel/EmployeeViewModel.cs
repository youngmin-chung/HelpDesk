using System;
using HelpDeskDAL;
using System.Reflection;
using System.Collections.Generic;

namespace HelpDeskViewModel
{
    public class EmployeeViewModel
    {
        private EmployeeModel _model;
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Id { get; set; }
        public bool? IsTech { get; set; }
        public string StaffPicture64 { get; set; }

        // constructor
        public EmployeeViewModel()
        {
            _model = new EmployeeModel();
        }

        //
        // collect Employee's information using Lastname property
        //

        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> allVms = new List<EmployeeViewModel>();
            try
            {
                List<Employee> allEmployee = _model.GetAll();
                foreach (Employee emp in allEmployee)
                {
                    EmployeeViewModel empVm = new EmployeeViewModel();
                    empVm.Title = emp.Title;
                    empVm.Firstname = emp.FirstName;
                    empVm.Lastname = emp.LastName;
                    empVm.Phoneno = emp.PhoneNo;
                    empVm.Email = emp.Email;
                    empVm.Id = emp.Id;
                    empVm.DepartmentId = emp.DepartmentId;
                    empVm.DepartmentName = emp.Department.DepartmentName;
                    empVm.IsTech = IsTech;
                    empVm.Timer = Convert.ToBase64String(emp.Timer);

                    if (emp.StaffPicture != null)
                    {
                        empVm.StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                    }
                    allVms.Add(empVm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }

        public List<EmployeeViewModel> GetTech()
        {
            List<EmployeeViewModel> vmList = new List<EmployeeViewModel>();
            try
            {
                List<Employee> allEmployee = _model.GetAll();
                foreach (Employee emp in allEmployee)
                {
                    if ((bool)emp.IsTech)
                    {
                        EmployeeViewModel evm = new EmployeeViewModel();
                        evm.DepartmentId = emp.DepartmentId;
                        evm.Email = emp.Email;
                        evm.Firstname = emp.FirstName;
                        evm.Id = emp.Id;
                        evm.Lastname = emp.LastName;
                        evm.Phoneno = emp.PhoneNo;
                        evm.Title = emp.Title;
                        evm.IsTech = emp.IsTech;
                        if (emp.StaffPicture != null)
                        {
                            evm.StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                        }
                        vmList.Add(evm);
                    }
                }//end foreach
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "GetAll");
            }
            return vmList;
        }//end GetTech

        //
        // find Employee using Lastname property
        //

        public void GetByLastname()
        {
            try
            {
                Employee emp = _model.GetByLastname(Lastname);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                IsTech = emp.IsTech;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "Not found";
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        //
        // Add a Employee
        //

        public void Add()
        {
            Id = -1;
            try
            {
                Employee emp = new Employee();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.DepartmentId = DepartmentId;
                emp.IsTech = IsTech;
                if (StaffPicture64 != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                Id = _model.Add(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        //
        // update a Employee
        //

        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                Employee emp = new Employee();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.Id = Id;
                emp.DepartmentId = DepartmentId;
                emp.IsTech = IsTech;
                if (StaffPicture64 != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                emp.Timer = Convert.FromBase64String(Timer);
                opStatus = _model.Update(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(opStatus);
        }

        //
        // Delete a Employee
        //

        public int Delete()
        {
            int employeeDeleted = -1;

            try
            {
                employeeDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return employeeDeleted;
        }

        //
        // find Employee using Is property
        //

        public void GetById()
        {
            try
            {
                Employee emp = _model.GetById(Id);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                IsTech = emp.IsTech;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "Not found";
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }



    }
}
