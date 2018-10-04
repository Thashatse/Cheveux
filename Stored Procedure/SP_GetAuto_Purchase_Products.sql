SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns all products and thier names from Auto_Purchase_Products table
-- =============================================
alter PROCEDURE SP_GetAuto_Purchase_Products
AS
BEGIN
	SELECT P.[Name]
	   ,AP.[ProductID]
      ,AP.[Qty]
	FROM [CHEVEUX].[dbo].[Auto_Purchase_Products] AP, PRODUCT P
	Where AP.[ProductID] = P.ProductID
	order by P.[Name]
END
GO
