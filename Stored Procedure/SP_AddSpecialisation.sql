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
alter PROCEDURE SP_AddSpecialisation 
	@employeeID nchar(30), 
	@serviceID nchar(10)
AS
BEGIN
BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO STYLIST_SERVICE
						(EmployeeID,ServiceID)
			VALUES		(@employeeID,@serviceID)			

		COMMIT TRANSACTION 
		END TRY 
		BEGIN CATCH 
			IF @@TRANCOUNT > 0 
				ROLLBACK TRANSACTION
		END CATCH 
END
GO
