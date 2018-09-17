SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the ditails of a specific order given the orderID
-- =============================================
alter PROCEDURE SP_GetProductOrder
	@OrderID nchar(10)
AS
BEGIN
	Select [OrderID]
      ,[OrderDate]
      ,[Received]
      ,[DateReceived] 
	  ,[Order].[SupplierID]
      ,[SupplierName]
      ,[ContactName]
      ,[ContactNo]
      ,[AddressLine1]
      ,[AddressLine2]
      ,[Suburb]
      ,[City]
      ,[ContactEmail] 
	From [Order], Supplier
	Where OrderID = @OrderID
		AND [Order].SupplierID = Supplier.SupplierID
	order by [OrderDate] 
END
GO
