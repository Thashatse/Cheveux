USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddUserGoogleAuth]    Script Date: 2018/05/12 13:48:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_AddUserGoogleAuth] 
	@ID nchar(30),
	@FN varchar(50),
	@LN varchar(50),
	@UN nvarchar(50),
	@EM varchar(50),
	@CN nchar(10),
	@UI nvarchar(50)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[User](UserID, FirstName, LastName, UserName, Email, ContactNo, UserType, Active, UserImage)
			Values(@ID, @FN, @LN, @UN, @EM, @CN, 'C', 'T', @UI)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
