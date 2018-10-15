SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the data for the sales gauge for a spacific Product
-- =============================================
alter PROCEDURE SP_SalesGaugeFroProduct 
	@prodID nchar(10)
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.ProductID = @prodID
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between dateadd(day, -30, getdate()) and GETDATE( ) 
  Group by sd.ProductID
END
GO
