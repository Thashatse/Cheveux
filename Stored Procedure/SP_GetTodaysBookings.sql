USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all of todays bookings and their details
-- =============================================
alter PROCEDURE [dbo].[SP_GetTodaysBookings]
AS
BEGIN
SELECT [BookingID]
      ,[BOOKING].SlotNo, [StartTime], [EndTime]
      ,[CustomerID],[FirstName], [LastName]
      ,[StylistID]
      ,[Date]
      ,[Available]
      ,[Arrived]
      ,[Comment]
  FROM [CHEVEUX].[dbo].[BOOKING], [CHEVEUX].[dbo].[TIMESLOT], [CHEVEUX].[dbo].[USER]
  Where BOOKING.SlotNo = TIMESLOT.SlotNo
		AND BOOKING.CustomerID = [USER].UserID
		AND [Date] = (SELECT CAST (GETDATE() AS DATE))
		AND BookingID = primaryBookingID
	Order by TIMESLOT.StartTime
END
