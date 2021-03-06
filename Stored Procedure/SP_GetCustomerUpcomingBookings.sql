USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookings]    Script Date: 2018/07/22 13:19:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_GetCustomerUpcomingBookings]
	@CustID nchar(30)
AS
BEGIN
Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived , B.StylistID       
	From BOOKING B, TIMESLOT TS, [User] U
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND B.Arrived = 'N'
	And B.BookingID = B.primaryBookingID
	Order by Date
END
