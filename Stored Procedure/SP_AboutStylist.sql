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
-- Description: Gives customer intro about the stylists that work for the salon. 
-- =============================================
CREATE PROCEDURE SP_AboutStylist
AS
BEGIN
	SET NOCOUNT ON;
	SELECT u.UserImage,
		   e.EmployeeID,(u.FirstName + ' ' + u.LastName)AS[StylistName],e.[Type],
		   s.ServiceID, p.[Name]AS[Specialisation], (p.ProductDescription)AS[SpecialisationDescription],
		   e.Bio
		   
	FROM [USER] u, EMPLOYEE e, STYLIST_SERVICE s, PRODUCT p
	
	WHERE e.EmployeeID=u.[UserID]
	AND   e.EmployeeID = s.EmployeeID
	AND   p.ProductID = s.ServiceID
	AND	  u.Active = 'T' 
	AND   u.UserType = 'E'
	AND   e.[Type] = 'S'
	
	ORDER BY StylistName asc

END
GO
