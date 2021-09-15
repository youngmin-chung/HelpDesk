using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HelpDeskDAL
{
    public class ProblemModel
    {
        private IRepository<Problem> repo;
        public ProblemModel()
        {
            repo = new HelpdeskRepository<Problem>();
        }

        public List<Problem> GetAll()
        {
            List<Problem> allProblems = new List<Problem>();
            try
            {
                allProblems = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allProblems;
        } //GetAll method

        public int Add(Problem newProblem)
        {
            try
            {
                newProblem = repo.Add(newProblem);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return newProblem.Id;
        }

        public int Delete(int id)
        {
            int problemsDeleted = -1;

            try
            {
                problemsDeleted = repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return problemsDeleted;
        }

        public Problem GetById(int id)
        {
            List<Problem> selectedProblem = null;
            try
            {
                selectedProblem = repo.GetByExpression(problem => problem.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedProblem.FirstOrDefault();
        }

        public Problem GetByDescription(string proDesc)
        {
            List<Problem> selectedProblem = null;
            try
            {
                selectedProblem = repo.GetByExpression(prb => prb.Description == proDesc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedProblem.FirstOrDefault(); ;
        } //GetByDesctription

        public UpdateStatus Update(Problem updatedProblem)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                opStatus = repo.Update(updatedProblem);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return opStatus;
        }
    }
}
