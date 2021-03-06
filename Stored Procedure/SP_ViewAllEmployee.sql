USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_ViewAllEmployee]    Script Date: 2018/07/29 16:58:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns a list of all employee details in the form of TypeLibary SP_ViewEmployee
-- =============================================
alter PROCEDURE [dbo].[SP_ViewAllEmployee]
AS
BEGIN
	Select UserID, FirstName, LastName, UserName, Email, ContactNo, [Type], UserImage, Active
	From [USER], EMPLOYEE
	Where UserID = EMPLOYEE.EmployeeID 
	AND [USER].Active = 'T'
	order by [Type] desc
END
