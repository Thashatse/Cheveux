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
alter PROCEDURE SP_UpdateEmployee
	@empID nchar(30),
	@type nchar(10),
	@bio varchar(max) = null,
	@addLine1 varchar(max),
	@addLine2 varchar(max) null,
	@suburb varchar(max),
	@city varchar(max)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			UPDATE [CHEVEUX].[dbo].[EMPLOYEE]
			SET [CHEVEUX].[dbo].[EMPLOYEE].[Type]=@type,
				[CHEVEUX].[dbo].[EMPLOYEE].Bio=@bio,
				[CHEVEUX].[dbo].[EMPLOYEE].AddressLine1=@addLine1,
				[CHEVEUX].[dbo].[EMPLOYEE].AddressLine2=@addLine2,
				[CHEVEUX].[dbo].[EMPLOYEE].Suburb=@suburb,
				[CHEVEUX].[dbo].[EMPLOYEE].City=@city
			WHERE EmployeeID=@empID

		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH   
END
GO
