USE [CHEVEUX]
GO
/****** Given a booking ID it Returns the booking detatils ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetCustomerUpcomingBookingDetails] 
	@BookingID nchar(10)
AS
BEGIN
	Select U.UserID,  U.FirstName, B.[Date], ts.SlotNo, TS.StartTime, BookingID           
	From BOOKING B, TIMESLOT TS, [User] U
	Where BookingID = @BookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived is null or B.Arrived = 'N') 
END