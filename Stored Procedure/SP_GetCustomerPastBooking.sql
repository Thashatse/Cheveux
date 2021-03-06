USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBooking]    Script Date: 2018/07/22 14:46:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_GetCustomerPastBooking]
	@CustID nchar(30)
AS
BEGIN
	Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived, B.StylistID       
	From BOOKING B, TIMESLOT TS, [User] U
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
	And B.BookingID = B.primaryBookingID
	Order by Date desc
END
