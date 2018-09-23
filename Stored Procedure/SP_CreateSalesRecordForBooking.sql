SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a bookingID Creates a sales record
-- =============================================
CREATE PROCEDURE SP_CreateSalesRecordForBooking
	@BookingID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			INSERt INTO SALE(SaleID, [Date], CustomerID, BookingID) 
				values(@BookingID, GETDATE(), (select CustomerID
								from BOOKING
								where BookingID = @BookingID), @BookingID)
								commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
