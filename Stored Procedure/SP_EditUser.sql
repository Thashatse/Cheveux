USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_EditUser]    Script Date: 2018/07/17 11:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Edits the Username and Contact No. of a user
-- =============================================
Create PROCEDURE [dbo].[SP_EditUser] 
	@UserID nchar(30),
	@UserName nchar(30),
	@ContactNo nchar(10),
	@Name varchar(50),
	@LName varchar(50),
	@Email varchar(50)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [User]
			SET UserName = @UserName,
				ContactNo = @ContactNo,
				FirstName = @Name,
				LastName = @LName,
				Email = @Email
			Where UserID = @UserID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
