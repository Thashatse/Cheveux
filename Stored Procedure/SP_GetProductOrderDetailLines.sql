SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the detail lines of a particular order
-- =============================================
alter PROCEDURE SP_GetProductOrderDetailLines
	-- Add the parameters for the stored procedure here
	@OrderID nchar(10)
AS
BEGIN
	Select [Order].[OrderID]
      ,[OrderDate]
      ,[Received]
      ,[DateReceived]
      ,Order_DTL.[ProductID]
	  ,[Name]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)] as ProductType
      ,[Active]
      ,[ProductImage]
      ,[Qty]
	  ,[Order].[SupplierID]
      ,[SupplierName]
      ,[ContactName]
      ,[ContactNo]
      ,[AddressLine1]
      ,[AddressLine2]
      ,[Suburb]
      ,[City]
      ,[ContactEmail] 
	From [Order], Order_DTL, Supplier, PRODUCT
	Where [Order].OrderID = Order_DTL.OrderID
		AND [Order].OrderID = @OrderID
		AND [Order].SupplierID = Supplier.SupplierID
		AND Order_DTL.ProductID = PRODUCT.ProductID
END
GO
