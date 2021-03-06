CREATE TABLE Deleted_Employees
(
    EmployeeId int PRIMARY KEY IDENTITY ,
     FirstName varchar(50) not NULL ,
     LastName varchar(50) not NULL ,
     MiddleName varchar(50),
     JobTitle varchar(50) not NULL ,
     DepartmentId int NOT NULL ,
     Salary money not NULL
)
GO

CREATE TRIGGER tr_InsertEmployeesOnDelete
    on Employees
    AFTER DELETE AS
    BEGIN
        INSERT INTO Deleted_Employees
        SELECT d.FirstName,d.LastName,d.MiddleName,d.JobTitle,d.DepartmentID,d.Salary
        FROM deleted as d
    END
GO