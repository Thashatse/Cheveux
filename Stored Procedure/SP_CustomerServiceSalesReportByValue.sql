SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_CustomerServiceSalesReportByValue 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] = 'S'
  Group by U.[UserName]
  order by [Value] desc
END
GO
