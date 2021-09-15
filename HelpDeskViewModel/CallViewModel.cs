using HelpDeskDAL;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HelpDeskViewModel
{
    public class CallViewModel
    {
        private CallModel _model;
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProblemId { get; set; }
        public string EmployeeName { get; set; }
        public string ProblemDescription { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public string Notes { get; set; }
        public string TechName { get; set; }
        public int TechId { get; set; }
        public bool OpenStatus { get; set; }
        public string Timer { get; set; }

        // Constructor
        public CallViewModel()
        {
            _model = new CallModel();
        }

        // Get all calls
        public List<CallViewModel> GetAll()
        {
            List<CallViewModel> allVms = new List<CallViewModel>();
            try
            {
                List<Call> allCalls = _model.GetAll();
                foreach (Call cal in allCalls)
                {
                    CallViewModel callVm = new CallViewModel();
                    callVm.EmployeeId = cal.EmployeeId;
                    callVm.ProblemId = cal.ProblemId;
                    callVm.DateOpened = cal.DateOpened;
                    callVm.DateClosed = cal.DateClosed;
                    callVm.OpenStatus = cal.OpenStatus;
                    callVm.Notes = cal.Notes;
                    callVm.TechId = cal.TechId;
                    callVm.Timer = Convert.ToBase64String(cal.Timer);
                    callVm.Id = cal.Id;

                    EmployeeModel eModel = new EmployeeModel();
                    //Employee emp = eModel.GetById(cal.Id);
                    Employee emp = eModel.GetById(cal.EmployeeId);
                    callVm.EmployeeName = emp.LastName;
                    callVm.TechName = eModel.GetById(cal.TechId).LastName;
                    callVm.ProblemDescription = cal.Problem.Description;
                    allVms.Add(callVm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;

        } //Getall

        //Add a call
        public void Add()
        {
            Id = -1;
            try
            {
                Call cal = new Call();
                cal.EmployeeId = EmployeeId;
                cal.ProblemId = ProblemId;
                cal.DateOpened = DateOpened;
                cal.DateClosed = DateClosed;
                cal.OpenStatus = OpenStatus;
                cal.Notes = Notes;
                cal.TechId = TechId;
                cal.Id = Id;
                Id = _model.Add(cal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        } //Add

        //Update a call
        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                Call cal = new Call();
                cal.EmployeeId = EmployeeId;
                cal.ProblemId = ProblemId;
                cal.DateOpened = DateOpened;
                cal.DateClosed = DateClosed;
                cal.OpenStatus = OpenStatus;
                cal.Notes = Notes;
                cal.TechId = TechId;
                cal.Id = Id;
                cal.Timer = Convert.FromBase64String(Timer);
                opStatus = _model.Update(cal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(opStatus);
        } //Update


        // Delete a call
        public int Delete()
        {
            int callsDeleted = -1;

            try
            {
                callsDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return callsDeleted;
        }


        // find call using getting by id property
        public void GetById()
        {
            try
            {
                Call cal = _model.GetById(Id);
                EmployeeId = cal.EmployeeId;
                ProblemId = cal.ProblemId;
                DateOpened = cal.DateOpened;
                DateClosed = cal.DateClosed;
                OpenStatus = cal.OpenStatus;
                Notes = cal.Notes;
                TechId = cal.TechId;
                Id = cal.Id;
                Timer = Convert.ToBase64String(cal.Timer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
    }
}
