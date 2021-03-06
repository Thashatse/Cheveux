USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteBooking]    Script Date: 2018/08/24 17:40:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_DeleteBooking] 
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
