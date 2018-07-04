SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all treatments and thier details from the product, treatment and brand tables
-- =============================================
CREATE PROCEDURE SP_GetAllTreatments
AS
BEGIN
	SELECT [ProductID]
      ,Product.Name
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[Active]
      ,[ProductImage]
	  ,[Qty]
      ,[TreatmentType]
      ,BRAND.[BrandID]
	  ,BRAND.Name
	  ,Brand.[Type(T/A)]
	FROM [CHEVEUX].[dbo].[PRODUCT], TREATMENT, BRAND
	WHERE ProductID = TreatmentID AND BRAND.BrandID = TREATMENT.BrandID
	order by [ProductType(T/A/S)]
END
GO
