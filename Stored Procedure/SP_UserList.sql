/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.1000)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_UserList]    Script Date: 7/14/2018 7:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_UserList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  u.UserImage,u.UserID, (u.FirstName + ' ' +u.LastName)AS [FullName] , u.UserName,u.Email,u.ContactNo
	FROM [USER] u
	WHERE u.UserType = 'C' OR u.UserType=null
	ORDER BY FullName ASC
END
