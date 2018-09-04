SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	adds payment type to existing sales record
-- =============================================
alter  PROCEDURE SP_AddPaymentTypeToSalesRecord
	@PaymentType nchar(10),
	@SaleD nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [SALE]
			SET PaymentType = @PaymentType
			Where SaleID = @SaleD
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
