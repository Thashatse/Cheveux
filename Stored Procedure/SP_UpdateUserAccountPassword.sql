SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Changes the password for the account matching the userID
-- =============================================
CREATE PROCEDURE SP_UpdateUserAccountPassword  
	@Password nvarchar(50),
	@UserID nchar(30)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [USER]
			SET PassRestCode = null,
				[Password] = @Password
			Where UserID = @UserID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
