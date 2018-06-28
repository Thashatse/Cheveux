USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns a list of all employee details in the form of TypeLibary SP_ViewEmployee
-- =============================================
CREATE PROCEDURE [dbo].[SP_ViewAllEmployee]
AS
BEGIN
	Select UserID, FirstName, LastName, UserName, Email, ContactNo, [Type], UserImage, Active
	From [USER], EMPLOYEE
	Where UserID = EMPLOYEE.EmployeeID
END
