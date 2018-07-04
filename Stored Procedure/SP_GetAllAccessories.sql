SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all accessories in the database with details from the product Table
-- =============================================
CREATE PROCEDURE SP_GetAllAccessories
AS
BEGIN
	SELECT [ProductID]
      ,[PRODUCT].[Name]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[Active]
      ,[ProductImage]
	  ,[Colour]
      ,[Qty]
      ,BRAND.[BrandID]
	  ,BRAND.Name
	  , BRAND.[Type(T/A)]
	FROM [CHEVEUX].[dbo].[PRODUCT], ACCESSORY, BRAND
	WHERE ProductID = AccessoryID AND BRAND.BrandID = ACCESSORY.BrandID
	order by [ProductType(T/A/S)]
END
GO
