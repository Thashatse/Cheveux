SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all past product stock orders
-- =============================================
Alter PROCEDURE SP_GetPastProductOrders
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
	Where Received = 'true'
		AND [Order].SupplierID = Supplier.SupplierID
	order by [DateReceived] DESC
END
GO
