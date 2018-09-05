SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
alter  PROCEDURE SP_AddEmployee
	@employeeID nchar(30),
	@bio varchar(max) = null,
	@AddressLine1 varchar(max) = null,
	@AddressLine2 varchar(max) = null,
	@suburb nchar(100) = null,
	@city nchar(100) = null,
	@firstname varchar(50),
	@lastname varchar(50),
	@username nvarchar(50),
	@email varchar(50),
	@contactNo nchar(10),
	@password varchar(max),
	@userimage varchar(max) = null,
	@passReset nchar(30) = null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION 
			INSERT INTO	dbo.[EMPLOYEE]
						(EmployeeID,[Type],Bio,AddressLine1,AddressLine2,Suburb,City)
			VALUES		(@employeeID,'S',@bio,@AddressLine1,@AddressLine2,@suburb,@city)
			COMMIT TRANSACTION
	END TRY
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH  

	BEGIN TRY 
		BEGIN TRANSACTION 
			INSERT INTO [USER]
						(UserID,FirstName,LastName,UserName,Email,ContactNo,
						[Password],UserType,Active,UserImage,AccountType,
						PreferredCommunication,PassRestCode)
			VALUES		(@employeeID,@firstname,@lastname,@username,@email,@contactNo,
							@password,'E','T',@userimage,'Email',
							'E',@passReset)
		COMMIT TRANSACTION 
	END TRY
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH  
END

GO
