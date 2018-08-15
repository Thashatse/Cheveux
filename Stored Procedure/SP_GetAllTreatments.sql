SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all treatments and thier details from the product, treatment and brand tables
-- =============================================
create PROCEDURE SP_GetAllTreatments
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
	  ,Brand.[Type(T/A)],
	  s.SupplierName,
	  s.ContactName,
	  s.ContactEmail,
	  s.ContactNo,
	  s.SupplierID
	FROM [CHEVEUX].[dbo].[PRODUCT], TREATMENT, BRAND, Supplier s
	WHERE ProductID = TreatmentID AND BRAND.BrandID = TREATMENT.BrandID AND s.SupplierID = TREATMENT.SupplierID 
	order by [ProductType(T/A/S)]
END
GO
