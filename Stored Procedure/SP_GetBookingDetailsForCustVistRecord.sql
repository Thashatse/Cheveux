SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets booking deatials for a booking in progress
-- =============================================
CREATE PROCEDURE SP_GetBookingDetailsForCustVistRecord
	-- Add the parameters for the stored procedure here
	@bookingID nchar(10),
	@customerID nchar(30)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID      
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where BookingID = @bookingID 
	AND CustomerID = @customerID
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
END
GO
