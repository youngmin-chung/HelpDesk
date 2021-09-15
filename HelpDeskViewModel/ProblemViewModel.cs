using HelpDeskDAL;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HelpDeskViewModel
{
    public class ProblemViewModel
    {
        private ProblemModel _model;
        public string Description { get; set; }
        public int Id { get; set; }
        public string Timer { get; set; }

        // Constructor
        public ProblemViewModel()
        {
            _model = new ProblemModel();
        }

        //retrieve all the problems
        public List<ProblemViewModel> GetAll()
        {
            List<ProblemViewModel> allVms = new List<ProblemViewModel>();
            try
            {
                List<Problem> problems = _model.GetAll();

                foreach (Problem pro in problems)
                {
                    ProblemViewModel proVm = new ProblemViewModel();
                    proVm.Id = pro.Id;
                    proVm.Description = pro.Description;
                    proVm.Timer = Convert.ToBase64String(pro.Timer);
                    allVms.Add(proVm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        } //GetAll 

        //find problem using description property
        public Problem GetByDescription()
        {
            try
            {
                Problem pro = _model.GetByDescription(Description);
                Id = pro.Id;
                Description = pro.Description;
                Timer = Convert.ToBase64String(pro.Timer);
                return pro;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        } //GetByDescription

        public void GetById()
        {
            try
            {
                Problem pro = _model.GetById(Id);
                Description = pro.Description;
                Id = pro.Id;
                Timer = Convert.ToBase64String(pro.Timer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public void Add()
        {
            Id = -1;
            try
            {
                Problem pro = new Problem();
                pro.Description = Description;
                pro.Id = Id;
                Id = _model.Add(pro);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        } //Add

        //update a problem
        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                Problem pro = new Problem();
                pro.Description = Description;
                pro.Id = Id;
                pro.Timer = Convert.FromBase64String(Timer);
                opStatus = _model.Update(pro);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(opStatus);
        } //Update


        // delete a problem
        public int Delete()
        {
            int problemsDeleted = -1;

            try
            {
                problemsDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return problemsDeleted;
        }
    }
}
