SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the data for the sales gauge
-- =============================================
alter PROCEDURE SP_SalesGaugeTotals
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.[ProductType(T/A/S)] != 'A'
	AND p.[ProductType(T/A/S)] != 'T'
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between dateadd(day, -30, getdate()) and GETDATE( )
  Group by sd.ProductID 
  order by [Value] asc
END
GO
