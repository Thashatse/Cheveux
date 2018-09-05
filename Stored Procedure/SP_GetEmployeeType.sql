SET ANSI_NULLS ON
GO 
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE SP_GetEmployeeType 
	@EmpID nchar(30)
AS
BEGIN
	Select [Type]
	from EMPLOYEE
	Where EmployeeID = @EmpID
END
GO
