SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets booking deatials for a booking in progress
-- =============================================
alter PROCEDURE SP_GetBookingDetailsForCustVistRecord
	@bookingID nchar(10)
AS
BEGIN
	Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.CustomerID, 
		(Select FirstName +' '+LastName
		From [USER]
		Where UserID = B.CustomerID) As custFullName
	From BOOKING B, TIMESLOT TS, [User] U
	Where BookingID = @bookingID
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.Arrived = 'Y'
END
GO
