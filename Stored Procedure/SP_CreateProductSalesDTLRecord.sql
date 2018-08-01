USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE SP_CreateProductSalesDTLRecord
	@SaleID nchar(10), 
	@ProductID nchar(10),
	@Qty int
AS
BEGIN
	begin try
		begin transaction
			INSERt INTO SALES_DTL(SaleID, ProductID, Qty, Price) 
				values(@SaleID, @ProductID, @Qty, (select Price from PRODUCT where ProductID = @ProductID))

				UPDATE TREATMENT
				SET Qty = (Select Qty From TREATMENT Where TreatmentID = @ProductID)-1
				WHERE TreatmentID = @ProductID

				UPDATE ACCESSORY
				SET Qty = (Select Qty From ACCESSORY Where AccessoryID = @ProductID)-1
				WHERE AccessoryID = @ProductID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
