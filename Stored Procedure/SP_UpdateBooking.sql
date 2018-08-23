SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Updates An Existing Booking
-- =============================================
alter PROCEDURE SP_UpdateBooking 
	@BookingID nchar(10),
	@SlotNo nchar(10),
	@StylistID nchar(30),
	@Date datetime
AS
BEGIN
	begin try
		begin transaction
			UPDATE BOOKING
			SET SlotNo = @SlotNo, 
				StylistID = @StylistID,
				[Date] = @Date,
				Arrived = 'N',
				NotificationReminder = 0
			Where (BookingID = @BookingID
					or  BOOKING.primaryBookingID = @BookingID)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
