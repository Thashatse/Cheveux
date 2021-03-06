USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTodaysTotalSales]    Script Date: 2018/08/24 16:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns total of todays sales
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetTodaysTotalSales]
AS
BEGIN
  SELECT sum(Qty * Price )
  FROM [CHEVEUX].[dbo].[SALE], SALES_DTL
  where SALE.SaleID = SALES_DTL.SaleID
		And [Date] > DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)
		And [Date] < DATEADD(day, DATEDIFF(day, -1, GETDATE()), 0)
END
