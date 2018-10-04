SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE SP_DeleteProdFromAutoPurch
	@ProductID nchar(10)
AS
BEGIN
		Begin Transaction;
			Begin Try
				DELETE FROM Auto_Purchase_Products
				Where [ProductID] = @ProductID
			End try
		Begin Catch
			if @@TRANCOUNT > 0
				Begin
					ROLLBACK TRANSACTION
				End
		End Catch
commit Transaction
END
GO
