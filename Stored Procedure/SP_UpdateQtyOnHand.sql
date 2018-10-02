
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE SP_UpdateQtyOnHand
	@ProductID nchar(10),
	@Qty int
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE ACCESSORY
			SET Qty = Qty + @Qty
			WHERE AccessoryID = @ProductID

			UPDATE TREATMENT
			SET Qty = Qty + @Qty
			WHERE TreatmentID = @ProductID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
