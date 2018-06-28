USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_ViewEmployee]    Script Date: 2018/06/28 12:03:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ViewEmployee] 
	@EmployeeID nchar(30)
AS
BEGIN
	Select UserID, FirstName, LastName, UserName, Email, ContactNo, [Type], UserImage, Active
	From [USER], EMPLOYEE
	Where UserID = EMPLOYEE.EmployeeID
		AND UserID = @EmployeeID
END
