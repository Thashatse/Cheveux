SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a the paramters Creates a sales record
-- =============================================
alter PROCEDURE SP_CreateSalesRecord 
	@SaleID nchar(10),
	@CustID nchar(30)
AS
BEGIN
	begin try
		begin transaction
			INSERt INTO SALE(SaleID, [Date], CustomerID, BookingID) 
				values(@SaleID, GETDATE(), @CustID, null)
			commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
