USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserDetails]    Script Date: 2018/07/17 12:39:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_GetUserDetails]
	@ID nchar(30)
AS
BEGIN
	Select FirstName, LastName, UserName, Email, ContactNo, UserType, UserImage, AccountType
	From [User]
	Where [User].[UserID] = @ID And Active = 'T'
END
