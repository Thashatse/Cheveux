SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Add reset code to user account withh matching email address or username
-- =============================================
CREATE PROCEDURE SP_CreateRestCode
	-- Add the parameters for the stored procedure here
	@restCode nchar(30),
	@Email varchar(50)
AS
BEGIN
begin try
		begin transaction
			UPDATE [USER]
			SET PassRestCode = @restCode
			Where Email = @Email
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
