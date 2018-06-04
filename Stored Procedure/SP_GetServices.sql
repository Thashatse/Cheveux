-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_GetServices
	@ProductID nchar(10)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Name
	FROM PRODUCT
	WHERE @ProductID = ProductID AND ProductType = 'S';
END
GO
