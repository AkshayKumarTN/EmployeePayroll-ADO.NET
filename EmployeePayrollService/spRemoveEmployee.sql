CREATE PROCEDURE spRemoveEmployee  
(  
@Id INT 
)  
as  
begin  
        DELETE FROM employee WHERE  Id = @id
        DELETE FROM Employee_Payroll WHERE  Id = @id
		DELETE FROM Payroll WHERE  Id = @id
		DELETE FROM EmployeeDepartment WHERE  Id = @id 
end 