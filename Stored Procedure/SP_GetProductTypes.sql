SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns the product type table
-- =============================================
alter PROCEDURE SP_GetProductTypes
AS
BEGIN
	SELECT [TypeID]
      ,[Name]
      ,[Product/Service]
	FROM [CHEVEUX].[dbo].[ProductType]
	order by [Name]
END
GO
