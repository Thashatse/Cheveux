SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Edits the Username and Contact No. of a user
-- =============================================
CREATE PROCEDURE SP_EditUser 
	@UserID nchar(30),
	@UserName nchar(30),
	@ContactNo nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [User]
			SET UserName = @UserName,
				ContactNo = @ContactNo
			Where UserID = @UserID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
