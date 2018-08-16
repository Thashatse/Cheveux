SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all accessories in the database with details from the product Table
-- =============================================
create PROCEDURE SP_GetAllAccessories
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
	  , BRAND.[Type(T/A)],
	  s.SupplierName,
	  s.ContactName,
	  s.ContactEmail,
	  s.ContactNo,
	  s.SupplierID
	FROM [CHEVEUX].[dbo].[PRODUCT], ACCESSORY, BRAND, Supplier s
	WHERE ProductID = AccessoryID AND BRAND.BrandID = ACCESSORY.BrandID AND s.SupplierID = ACCESSORY.SupplierID 
	order by [ProductType(T/A/S)]
END
GO
