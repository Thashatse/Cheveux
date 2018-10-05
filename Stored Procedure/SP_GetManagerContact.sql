SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns the contact details of the manager
-- =============================================
alter PROCEDURE SP_GetManagerContact
AS
BEGIN
	Select Email, ContactNo
	From [USER], EMPLOYEE
	where userType = 'E'
		AND EMPLOYEE.Type = 'M'
		AND [USER].UserID = EmployeeID
END
GO
