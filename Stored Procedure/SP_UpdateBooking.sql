SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Updates An Existing Booking
-- =============================================
CREATE PROCEDURE SP_UpdateBooking 
	@BookingID nchar(10),
	@SlotNo nchar(10),
	@StylistID nchar(30),
	@ServiceID nchar(10),
	@Date datetime
AS
BEGIN
	begin try
		begin transaction
			UPDATE BOOKING
			SET SlotNo = @SlotNo, 
				StylistID = @StylistID,
				ServiceID = @ServiceID,
				[Date] = @Date,
				Arrived = 'N'
			Where BookingID = @BookingID 
			AND (select Available
					from BOOKING
					Where Date = @Date
					AND SlotNo = @SlotNo
					AND StylistID = @StylistID) = 'Y'
					commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
