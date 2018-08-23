SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Bookings report for hairstylist with date range
-- =============================================
alter PROCEDURE SP_BookingsReportForHairstylistwithDateRange
	@stylistID nchar(30),
	@startDate datetime,
	@endDate   datetime
AS
BEGIN
	Select BookingID, 
		TIMESLOT.SlotNo, StartTime,EndTime, 
		CustomerID,FirstName,LastName, 
		StylistID, 
		[SERVICE].ServiceID, PRODUCT.Name, 
		Date, Available, Arrived, Comment
	From BOOKING, TIMESLOT, [USER], [SERVICE] , [PRODUCT]
	where TIMESLOT.SlotNo = BOOKING.SlotNo
		And BOOKING.CustomerID = [USER].UserID
		And BOOKING.ServiceID = [SERVICE].ServiceID
		And [SERVICE].ServiceID = PRODUCT.ProductID
		and BOOKING.StylistID =  @stylistID
		and Date BETWEEN @startDate   AND @endDate
		And BOOKING.BookingID = BOOKING.primaryBookingID
END
GO


