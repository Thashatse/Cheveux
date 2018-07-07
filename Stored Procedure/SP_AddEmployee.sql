/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.1000)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddEmployee]    Script Date: 7/7/2018 1:49:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 06.07.2018
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddEmployee]
	@EmpID nchar(30),
	@FirstName varchar(50),
	@LastName varchar(50),
	@UserName nvarchar(50),
	@Email varchar(50),
	@ContactNo nchar(10), 
	@UserImage varchar(max) = null,
	@AccountType nchar(10) = null,
	@AddressLine1 varchar(max),
	@AddressLine2 varchar(max),
	@EmpType nchar(10)

AS
BEGIN

	BEGIN TRY 
		BEGIN TRANSACTION 

			INSERT INTO dbo.[USER]
						(UserID,FirstName,LastName,UserName,
						 Email,ContactNo,UserType,Active,
						 UserImage,AccountType)
			VALUES		(@EmpID,@FirstName,@LastName,@UserName,
						 @Email,@ContactNo,'E','T',
						 @UserImage,@AccountType)

			INSERT INTO dbo.[EMPLOYEE] 
						(EmployeeID,AddressLine1,AddressLine2,[Type])
			VALUES		(@EmpID,@AddressLine1,@AddressLine2,@EmpType)


		COMMIT TRANSACTION 
	END TRY
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 
END
