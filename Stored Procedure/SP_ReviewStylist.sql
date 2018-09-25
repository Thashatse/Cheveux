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
-- Author:		S.MAQABANGQA
-- =============================================
alter PROCEDURE SP_ReviewStylist
	@reviewID nchar(10),
	@customerID nchar(30),
	@employeeID nchar(30),
	@date datetime,
	@time time(7),
	@rating float = null,
	@comment varchar(50) = null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION 
			INSERT INTO REVIEW
					   (ReviewID,CustomerID,
					   EmployeeID,
					   [Date],[Time],
					   Rating,Comment)
			VALUES	   (@reviewID,@customerID,
						@employeeID,
						@date,@time,@rating,
						@comment)
		COMMIT TRANSACTION
		END TRY
	BEGIN CATCH
		if @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
