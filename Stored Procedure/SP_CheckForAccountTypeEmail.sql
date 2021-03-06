USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForAccountTypeEmail]    Script Date: 2018/07/28 12:48:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a username or email address returns the aaccountype
-- =============================================
ALTER PROCEDURE [dbo].[SP_CheckForAccountTypeEmail]
	@identifier nvarchar(50)
AS
BEGIN
	Select AccountType
	From [USER]
	Where (Email = @identifier
		or UserName = @identifier)
		AND [USER].Active = 'T'
END
