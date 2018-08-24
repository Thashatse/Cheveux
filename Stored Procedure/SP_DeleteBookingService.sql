SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Removes Booking Records and Booking Service matchin the BookingID or primaryBookingID
-- =============================================
Create PROCEDURE SP_DeleteBookingService 
	@BookingID nchar(10),
	@ServiceID nchar(10)
AS
BEGIN
		Begin Transaction;
			Begin Try
				DELETE FROM BookingService
				Where BookingID = @BookingID
						AND ServiceID = @ServiceID
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
