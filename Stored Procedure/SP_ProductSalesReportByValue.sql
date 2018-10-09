SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value
-- =============================================
CREATE PROCEDURE SP_ProductSalesReportByValue 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.[ProductType(T/A/S)] != 'S'
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between @StartDate and @EndDate
  Group by sd.ProductID
  order by [Value] desc
END
GO
