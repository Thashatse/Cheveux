USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter  PROCEDURE SP_RemoveProductSalesDTLRecord
	@SaleID nchar(10), 
	@ProductID nchar(10),
	@Qty int
AS
BEGIN
	begin try
		begin transaction
				Delete From SALES_DTL
				Where ProductID = @ProductID
					AND SaleID = @SaleID

				UPDATE TREATMENT
				SET Qty = (Select Qty+@Qty From TREATMENT Where TreatmentID = @ProductID)
				WHERE TreatmentID = @ProductID

				UPDATE ACCESSORY
				SET Qty = (Select Qty+@Qty From ACCESSORY Where AccessoryID = @ProductID)
				WHERE AccessoryID = @ProductID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
