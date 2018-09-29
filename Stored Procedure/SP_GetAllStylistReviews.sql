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
CREATE PROCEDURE SP_GetAllStylistReviews
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ReviewID,CustomerID,EmployeeID,primaryBookingID,[Date],[Time],Rating,Comment
	FROM REVIEW
	WHERE primaryBookingID is null
END
GO
