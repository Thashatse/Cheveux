SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returnst the password for the user account matching the username or emailaddress
-- =============================================
alter  PROCEDURE SP_GetPasHash
	@identifier nvarchar(50)
AS
BEGIN
	Select [Password]
	From [USER]
	Where (Email = @identifier
		or UserName = @identifier)
		AND [USER].Active = 'T'
END
GO
