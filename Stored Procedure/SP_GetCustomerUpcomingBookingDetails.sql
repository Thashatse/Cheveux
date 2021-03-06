USE [CHEVEUX]
GO
/****** Given a booking ID it Returns the booking detatils ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_GetCustomerUpcomingBookingDetails] 
	@BookingID nchar(10)
AS
BEGIN
	Select U.UserID,  U.FirstName, B.[Date], ts.SlotNo, TS.StartTime, BookingID, 
	b.CustomerID, (select FirstName + ' '+ LastName from [USER] where [USER].UserID = b.CustomerID) as CustFullName, B.Comment           
	From BOOKING B, TIMESLOT TS, [User] U
	Where BookingID = @BookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived is null or B.Arrived = 'N') 
END