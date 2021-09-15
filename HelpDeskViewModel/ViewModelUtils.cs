using HelpDeskDAL;
using System;
using System.Diagnostics;
using System.Reflection;

namespace HelpDeskViewModel
{
    public class ViewModelUtils
    {
        public static void ErrorRoutine(Exception e, string obj, string method)
        {
            if (e.InnerException != null)
            {
                Trace.WriteLine("Error in ViewModels, object=" + obj +
                                    ", method=" + method +
                                    " , inner exception message=" +
                                    e.InnerException.Message);
                throw e.InnerException;
            }//end if
            else
            {
                Trace.WriteLine("Error in ViewModels, object=" + obj +
                                    ", method=" + method + " , message=" +
                                    e.Message);
                throw e;
            }
        }//end errorRoutine

        public bool LoadCollections()
        {
            bool createOk = false;

            try
            {
                DALUtil dalUtil = new DALUtil();
                createOk = dalUtil.AddEmployeePicsToDb();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return createOk;
        }//end LoadCollections()

    }//end class
}
