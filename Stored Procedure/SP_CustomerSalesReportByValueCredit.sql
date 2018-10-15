SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value fore credit sales
-- =============================================
CREATE PROCEDURE SP_CustomerSalesReportByValueCredit 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Credit'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
  Group by U.[UserName]
  order by [Value] desc
END
GO