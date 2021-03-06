USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateUserAccountPassword]    Script Date: 2018/08/10 16:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Changes the password for the account matching the userID
-- =============================================
alter  PROCEDURE [dbo].[SP_UpdateUserAccountPassword]  
	@Password nvarchar(max),
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
