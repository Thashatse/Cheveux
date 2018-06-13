SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a bookingID Creates a sales record
-- =============================================
CREATE PROCEDURE SP_CreateSalesRecord 
	@BookingID nchar(10),
	@PaymentType nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE SALE
			SET SaleID = @BookingID,
				[Date] = GETDATE(),
				CustomerID = (select CustomerID
								from BOOKING
								where BookingID = @BookingID),
				PaymentType = @PaymentType,
				BookingID = @BookingID
			Where BookingID = @BookingID 
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
