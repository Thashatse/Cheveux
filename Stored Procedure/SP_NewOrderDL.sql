SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	creates a new order detail line give the paramaters
-- =============================================
CREATE PROCEDURE SP_NewOrderDL 
	@OrderID nchar(10),
	@ProdID nchar(10), 
	@Qty int
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[Order_DTL](OrderID, ProductID, Qty)
			Values(@OrderID, @ProdID, @Qty)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
