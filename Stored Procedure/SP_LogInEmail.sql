USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_LogInEmail]    Script Date: 2018/08/10 16:11:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	using the email of password returns  UserID, UserType, [FirstName]
-- =============================================
alter  PROCEDURE [dbo].[SP_LogInEmail] 
	-- Add the parameters for the stored procedure here
	@identifier nvarchar(50),
	@password nvarchar(max)
AS
BEGIN
	Select UserID, UserType, [FirstName]
	From [USER]
	Where Password = @password
	AND (Email = @identifier
		or UserName = @identifier)
		AND [USER].Active = 'T'
END
