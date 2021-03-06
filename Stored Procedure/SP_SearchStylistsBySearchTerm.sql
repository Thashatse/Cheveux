USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchStylistsBySearchTerm]    Script Date: 2018/07/28 12:46:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_SearchStylistsBySearchTerm]
	@searchTerm varchar(50)
AS
BEGIN
	Select U.UserID, U.FirstName, U.LastName, U.UserImage
	From [User] U, EMPLOYEE E
	Where (U.FirstName like '%'+@searchTerm+'%'  Or U.LastName like '%'+@searchTerm+'%')
	And U.UserID = E.EmployeeID 
	And E.Type= 'S' 
	AND U.Active = 'T'
END
