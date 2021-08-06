CREATE PROCEDURE spAddEmployee  
(  
@Id INT ,
@Name VARCHAR(30),
@Salary money ,
@StartDate DATE,
@Gender CHAR(1),
@PhoneNumber varchar(15),
@Address varchar(40) ,
@Department varchar(10),
@Deduction money,
@TaxablePay money,
@IncomeTax money,
@NetPay money
)  
as  
begin  
        insert into Employee_Payroll values(@Id,@Name,@Salary,GETDATE(),@Gender,@PhoneNumber,@Address,@Department,@Deduction,@TaxablePay,@IncomeTax,@NetPay)
		insert into Employee values(@Id,@Name,@Gender,@PhoneNumber,@Address)
        insert into Payroll values(@Id,GETDATE(),@Salary,@Deduction,@TaxablePay,@IncomeTax,@NetPay)
        insert into EmployeeDepartment values(@Id,@Department)  
end