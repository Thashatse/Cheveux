SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns the stock management Table
-- =============================================
alter PROCEDURE SP_GetStockManagementSettings
AS
BEGIN
	SELECT [LowStock]
      ,[PurchaseQty]
      ,[AutoPurchase]
      ,[AutoPurchaseFrequency]
      ,[AutoPurchaseProducts]
      ,[BusinessID]
	  ,[NxtOrderDate]
	FROM [CHEVEUX].[dbo].[Stock_Management]
END
GO
