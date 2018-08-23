SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Removes Booking Records and Booking Service matchin the BookingID or primaryBookingID
-- =============================================
alter PROCEDURE SP_DeleteBooking 
	@BookingID nchar(10)
AS
BEGIN
		Begin Transaction;
			Begin Try
				DELETE FROM BOOKING
				Where (BookingID = @BookingID
					or  BOOKING.primaryBookingID = @BookingID)

				DELETE FROM BookingService
				Where BookingID = @BookingID
			End try
		Begin Catch
			if @@TRANCOUNT > 0
				Begin
					ROLLBACK TRANSACTION
				End
		End Catch
commit Transaction
END
GO
