USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBookingDetail]    Script Date: 2018/08/04 11:25:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a booking ID it reaturns the deatails of that booking
-- =============================================
alter  PROCEDURE [dbo].[SP_GetCustomerPastBookingDetail] 
	@bookingID nchar(10)
AS
BEGIN
	Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived , B.StylistID, 
	b.CustomerID, (select FirstName + ' '+ LastName from [USER] where [USER].UserID = b.CustomerID) as CustFullName        
	From BOOKING B, TIMESLOT TS, [User] U
	Where BookingID = @bookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
END
