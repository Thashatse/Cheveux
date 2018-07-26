-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE SP_AddEmployee
	@employeeID nchar(30),
	@AddressLine1 varchar(max) = null,
	@AddressLine2 varchar(max) = null,
	@Type nchar(10)
AS
BEGIN

	BEGIN TRY 
		BEGIN TRANSACTION 
			INSERT INTO EMPLOYEE
						(EmployeeID,AddressLine1,AddressLine2,[Type])
			VALUES		(@employeeID,@AddressLine1,@AddressLine2,@Type)

			UPDATE [USER]
			SET	   [USER].UserType = 'E'
			WHERE  [USER].UserID = @employeeID
		COMMIT TRANSACTION 
	END TRY
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 
END

GO
