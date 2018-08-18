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
CREATE PROCEDURE SP_UpdateEmployee
	@empID nchar(30),
	@type nchar(10),
	@addLine1 varchar(max) = null,
	@addLine2 varchar(max) = null,
	@suburb nchar(100) = null,
	@city nchar(100) = null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			UPDATE EMPLOYEE
			SET [Type]=@type,
				AddressLine1=@addLine1,
				AddressLine2=@addLine2,
				Suburb=@suburb,
				City=@city
			WHERE EmployeeID=@empID

		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH  
END
GO
