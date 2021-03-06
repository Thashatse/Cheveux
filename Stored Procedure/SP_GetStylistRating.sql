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
alter PROCEDURE SP_GetStylistRating
	@stylistID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROUND(AVG(Rating),0) as AverageRating 
	FROM [CHEVEUX].[dbo].[REVIEW]
	WHERE EmployeeID=@stylistID
	AND primaryBookingID IS NULL
	AND [CHEVEUX].[dbo].[REVIEW].[Date] !< DATEADD(mm, -2, GETDATE())
END
GO
