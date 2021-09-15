using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HelpDeskDAL
{
    // Call Model
    public class CallModel
    {
        private IRepository<Call> repo;
        public CallModel()
        {
            repo = new HelpdeskRepository<Call>();
        }
        //Get All
        public List<Call> GetAll()
        {
            List<Call> allCalls = new List<Call>();

            try
            {
                allCalls = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allCalls;
        }
        // Get By Id
        public Call GetById(int id)
        {
            List<Call> selectedCall = null;
            try
            {
                selectedCall = repo.GetByExpression(call => call.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedCall.FirstOrDefault();
        }

        // Add
        public int Add(Call newCall)
        {
            try
            {
                newCall = repo.Add(newCall);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return newCall.Id;
        }

        // Update
        public UpdateStatus Update(Call updatedCall)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                opStatus = repo.Update(updatedCall);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return opStatus;
        }

        // Delete
        public int Delete(int id)
        {
            int callsDeleted = -1;

            try
            {
                callsDeleted = repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return callsDeleted;
        }
    }
}