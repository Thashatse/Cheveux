USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_ViewEmployee]    Script Date: 2018/06/25 12:14:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	given an emplyeeID it returns the employee details
-- =============================================
create PROCEDURE [dbo].[SP_ViewEmployee] 
	@EmployeeID nchar(30)
AS
BEGIN
	Select UserID, FirstName, LastName, UserName, Email, ContactNo, [Type], UserImage
	From [USER], EMPLOYEE
	Where UserID = EMPLOYEE.EmployeeID
		AND Active = 'T'
		AND UserID = @EmployeeID
END
