SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all outstanding product stock orders
-- =============================================
alter PROCEDURE SP_GetOutstandingProductOrders
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
	Where Received = 'false'
		AND [Order].SupplierID = Supplier.SupplierID
END
GO
