USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchForUser]    Script Date: 2018/07/28 12:46:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
ALTER PROCEDURE [dbo].[SP_SearchForUser]
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
		   AND u.Active = 'T'
		ORDER BY (u.FirstName + ' ' +u.LastName) ASC
END
