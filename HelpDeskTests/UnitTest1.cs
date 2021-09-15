using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpDeskDAL;

namespace HelpDeskTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoadPicsShouldReturnTrue()
        {
            DALUtil util = new DALUtil();
            Assert.IsTrue(util.AddEmployeePicsToDb());
        }
    }
}
