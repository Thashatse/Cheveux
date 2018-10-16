SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume for cash sales
-- =============================================
alter PROCEDURE SP_ProductSalesReportByVolumeCash 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND sd.SaleID = s.SaleID
	AND p.[ProductType(T/A/S)] != 'S'
	AND s.[Date]  between @StartDate and @EndDate
	AND s.PaymentType = 'Cash'
  Group by sd.ProductID
  order by [Volume] desc
END
GO
