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
alter PROCEDURE SP_CustomerSatisfaction 
	@startDate datetime = null,
	@endDate datetime =null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Distinct(r.CustomerID),(u.FirstName + ' ' + u.LastName)as[CustomerName],
		   AVG(Rating)as Rating, COUNT(r.CustomerID) as NoOfReviews
	FROM REVIEW r, [USER] u
	where r.CustomerID=u.UserID
	and   r.[Date] between @startDate and @endDate
	GROUP BY r.CustomerID,u.FirstName,u.LastName
	ORDER BY Rating desc
END
GO
