SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Set the active colum of a user acount to false
-- =============================================
alter  PROCEDURE SP_DeactivateUser 
	@UserID nchar(30)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [USER]
			SET Active = 'F'
			Where [UserID] = @UserID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
