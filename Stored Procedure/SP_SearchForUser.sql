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
CREATE PROCEDURE SP_SearchForUser
	@searchTerm varchar(50)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT u.UserImage,u.UserID, (u.FirstName + ' ' +u.LastName)AS[FullName] , u.UserName,u.Email,u.ContactNo
	FROM [USER] u
	WHERE (u.UserImage like '%'+@searchTerm+'%'
		   Or u.UserID like '%'+@searchTerm+'%'  
		   Or u.FirstName like '%'+@searchTerm+'%'
		   Or u.LastName like '%'+@searchTerm+'%'
		   Or u.FirstName + ' ' + u.LastName like '%'+@searchTerm+'%'
		   Or u.UserName like '%'+@searchTerm+'%'
		   Or u.Email like '%'+@searchTerm+'%'
		   Or u.ContactNo like '%'+@searchTerm+'%')
END
GO
