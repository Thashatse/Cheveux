USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_ViewEmployee]    Script Date: 2018/07/26 15:52:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_ViewEmployee] 
	@EmployeeID nchar(30)
AS
BEGIN
	Select UserID, FirstName, LastName, UserName, Email, ContactNo, [Type], UserImage, Active, e.AddressLine1, e.AddressLine2, e.Suburb, e.City
	From [USER], EMPLOYEE e
	Where UserID = e.EmployeeID
		AND UserID = @EmployeeID
END
