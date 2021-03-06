USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_UserList]    Script Date: 2018/08/16 16:18:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
alter PROCEDURE [dbo].[SP_UserList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  u.UserImage,u.UserID, (u.FirstName + ' ' +u.LastName)AS [FullName] , u.UserName,u.Email,u.ContactNo, u.UserType
	FROM [USER] u
	WHERE (u.UserType = 'C' OR u.UserType=null)
		AND u.Active = 'T'
	ORDER BY FullName ASC
END
