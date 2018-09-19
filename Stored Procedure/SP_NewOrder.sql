SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	craetes a new order record given the corect paramaters
-- =============================================
CREATE PROCEDURE SP_NewOrder 
	@OrderID nchar(10),
	@SuppID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[Order](OrderID, OrderDate, SupplierID, Received)
			Values(@OrderID, GETDATE(), @SuppID, 0)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
