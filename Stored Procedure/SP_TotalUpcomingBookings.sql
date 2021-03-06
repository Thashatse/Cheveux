USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_TotalUpcomingBookings]    Script Date: 2018/08/24 16:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets the total number of upcoming bookings
-- =============================================
ALTER PROCEDURE [dbo].[SP_TotalUpcomingBookings]
AS
BEGIN
	SELECT count(*)
	FROM BOOKING
	where [Date] > DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)
		And Arrived = 'N'
		And BookingID = primaryBookingID
END
