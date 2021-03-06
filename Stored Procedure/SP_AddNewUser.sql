USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter  PROCEDURE [dbo].[SP_AddNewUser] 
	@ID nchar(30),
	@FN varchar(50), 
	@LN varchar(50),
	@UN nvarchar(50),
	@EM varchar(50),
	@CN nchar(10) = null,
	@UI nvarchar(MAX) = null,
	@AT nchar(10),
	@pass nvarchar(MAX) = null,
	@UT char(1)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[User](UserID, FirstName, LastName, UserName, Email, ContactNo, UserType, Active, UserImage, AccountType, [Password])
			Values(@ID, @FN, @LN, @UN, @EM, @CN, @UT, 'T', @UI, @AT, @pass)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
