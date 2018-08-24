USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_TotalBookings]    Script Date: 2018/08/24 16:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets the total number of all time bookings
-- =============================================
ALTER PROCEDURE [dbo].[SP_TotalBookings]
AS
BEGIN
	SELECT count(*)
	FROM BOOKING
	where BookingID = primaryBookingID
END
