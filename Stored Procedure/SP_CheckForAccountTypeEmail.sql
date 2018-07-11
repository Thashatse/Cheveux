USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForAccountEmail]    Script Date: 2018/07/11 13:44:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a username or email address returns the aaccountype
-- =============================================
Create PROCEDURE [dbo].[SP_CheckForAccountTypeEmail]
	@identifier nvarchar(50)
AS
BEGIN
	Select AccountType
	From [USER]
	Where Email = @identifier
		or UserName = @identifier
END
