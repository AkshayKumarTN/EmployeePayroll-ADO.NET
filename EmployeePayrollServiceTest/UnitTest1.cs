using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeePayrollService;

namespace EmployeePayrollServiceTest
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepo employeeRepo;
        EmployeeModel model;
        [TestInitialize]
        public void SetUp()
        {
            employeeRepo = new EmployeeRepo();
            model = new EmployeeModel();
        }

        [TestMethod]
        public void TestUpdateSalary()
        {
            int expected = 2;
            int actual = employeeRepo.UpdateSalary(model);
            Assert.AreEqual(actual, expected);
        }

        //
        [TestMethod]
        public void TestUpdateSalaryUsingPreparedStatement()
        {
            int expected = 0;
            model.EmployeeID = 5;
            model.EmployeeName = "Terisa";
            model.BasicPay = 30000000;
            int actual = employeeRepo.UpdateSalaryUsingPreparedStatement(model);
            Assert.AreEqual(actual, expected);
        }

    }
}
