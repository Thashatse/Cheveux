USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Removes all secondary bookings matching primary bookingIDs
-- =============================================
alter PROCEDURE SP_DeleteSecondaryBookings
	@PrimaryBookingID nchar(10)
AS
BEGIN
	Begin Transaction;
		Begin Try
			DELETE FROM BOOKING
			Where BookingID != @PrimaryBookingID
					AND primaryBookingID = @PrimaryBookingID
		End try
	Begin Catch
		if @@TRANCOUNT > 0
			Begin
				ROLLBACK TRANSACTION
			End
		End Catch
commit Transaction
END
