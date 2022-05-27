SELECT DepartmentID, MIN(Salary) as MinimumSalary
	FROM Employees
	WHERE DepartmentID IN(2,5,7)
	GROUP BY DepartmentID
	ORDER BY DepartmentID