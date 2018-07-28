USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForUserType]    Script Date: 2018/07/28 12:43:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_CheckForUserType] 
	@ID nchar(30)
AS
BEGIN
	SELECT UserType
	From [CHEVEUX].[dbo].[User]
	Where UserID = @ID
	AND [USER].Active = 'T'
END
