SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets booking deatials for a booking in progress
-- =============================================
create PROCEDURE SP_GetBookingDetailsForCustVistRecord
	@bookingID nchar(10)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.CustomerID, B.ServiceID, 
		(Select FirstName +' '+LastName
		From [USER]
		Where UserID = B.CustomerID) As custFullName
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where BookingID = @bookingID
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND B.Arrived = 'Y'
END
GO
