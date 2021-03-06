USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_TotalBksMissedByCustomers]    Script Date: 10/14/2018 8:38:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	 S.MAQABANGQA
-- Description:	Returns to total number of bookings missed by each customer
-- =============================================
alter PROCEDURE SP_TotalBksMissedByCustomers
	@startDate datetime = null,
	@endDate datetime = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Distinct(b.CustomerID),(u.FirstName + ' ' + u.LastName)as[CustomerName],
		COUNT(b.BookingID)as[BookingsMissed]
	FROM BOOKING b, [USER] u
	WHERE b.BookingID= primaryBookingID
	and   b.CustomerID=u.UserID
	and   u.UserType='C'
	and   b.[Date]  between @startDate and @endDate
	and   b.Arrived = 'N'
	GROUP BY b.CustomerID,u.FirstName,u.LastName
	ORDER BY BookingsMissed desc
END
