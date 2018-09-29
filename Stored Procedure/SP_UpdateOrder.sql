
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_UpdateOrder
	@OrderID nchar(10),
	@DateReceived datetime,
	@Received bit
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [Order]
			SET DateReceived = @DateReceived,
				Received = @Received
			WHERE OrderID = @OrderID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
