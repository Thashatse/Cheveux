-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the detail lines of a particular order
-- =============================================
Alter PROCEDURE SP_GetProductOrderDetailLines
	-- Add the parameters for the stored procedure here
	@OrderID nchar(10)
AS
BEGIN
	Select * 
	From [Order], Order_DTL, Supplier, PRODUCT
	Where [Order].OrderID = Order_DTL.OrderID
		AND [Order].OrderID = @OrderID
		AND [Order].SupplierID = Supplier.SupplierID
		AND Order_DTL.ProductID = PRODUCT.ProductID
END
GO
