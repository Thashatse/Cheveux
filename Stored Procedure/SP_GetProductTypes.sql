USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductTypes]    Script Date: 2018/10/12 11:26:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns the product type table
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetProductTypes]
AS
BEGIN
	SELECT [TypeID]
      ,[Name]
      ,[Product/Service]
	  ,[PrimaryService]
	FROM [CHEVEUX].[dbo].[ProductType]
	order by [Name]
END
