SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
alter PROCEDURE SP_UpdateStockManagementSettings
	@BusinessID nchar(10),
	@LowStock int,
	@PurchaseQty int,
	@AutoPurchase bit,
	@AutoPurchaseFrequency nchar(3),
	@AutoPurchaseProducts bit,
	@nextDate DateTime
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [Stock_Management]
			SET LowStock = @LowStock,
				PurchaseQty = @PurchaseQty,
				AutoPurchase = @AutoPurchase,
				AutoPurchaseFrequency = @AutoPurchaseFrequency,
				AutoPurchaseProducts = @AutoPurchaseProducts,
				[NxtOrderDate] = @nextDate
			WHERE BusinessID = @BusinessID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
