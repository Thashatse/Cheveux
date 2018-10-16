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
CREATE PROCEDURE SP_MostPopularStylist
	@startDate datetime = null,
	@endDate datetime =null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Distinct(r.EmployeeID),(u.FirstName + ' ' + u.LastName)as[EmployeeName],
		   AVG(Rating)as Rating
	FROM [CHEVEUX].[dbo].REVIEW r, [USER] u
	where r.EmployeeID=u.UserID
	and   r.[Date] between @startDate and @endDate
	and   r.primaryBookingID is null
	GROUP BY r.EmployeeID,u.FirstName,u.LastName
	ORDER BY Rating desc
END
GO
