SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns acount details for the account matchingf the reset code
-- =============================================
CREATE PROCEDURE SP_GetAccountForRestCode 
	@Code nchar(30)
AS
BEGIN
	SELECT UserName, UserID
	from [USER]
	where PassRestCode = @Code
END
GO
