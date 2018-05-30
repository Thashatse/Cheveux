SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_DeleteBooking 
	@BookingID nchar(10)
AS
BEGIN
		Begin Transaction;
			Begin Try
				DELETE FROM BOOKING
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
