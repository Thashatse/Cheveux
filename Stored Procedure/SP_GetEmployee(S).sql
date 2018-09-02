-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE SP_GetEmployee(S)
@EmployeeID nchar(30)
AS
BEGIN
	Select u.UserImage,u.UserID, u.FirstName, u.LastName, u.UserName, u.Email, u.ContactNo, e.[Type], u.UserImage, 
		   u.Active, e.AddressLine1, e.AddressLine2, e.Suburb, e.City,e.Bio, ss.ServiceID,
		   p.[Name]AS[Specialisation], (p.ProductDescription)AS[SpecialisationDescription]
	From [USER] u, EMPLOYEE e, STYLIST_SERVICE ss,PRODUCT p
	Where u.UserID = @EmployeeID
		AND u.UserID = e.EmployeeID
		AND e.EmployeeID=@EmployeeID
		AND   p.ProductID = ss.ServiceID
		AND   u.UserType = 'E'
		AND   ss.EmployeeID=@EmployeeID
END
GO
