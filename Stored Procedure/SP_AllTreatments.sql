USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_Products]    Script Date: 8/11/2018 4:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[SP_Products]

@productID nchar(10)


AS
BEGIN
	
SELECT ProductID, Product.Name, ProductDescription, Price, [ProductType(T/A/S)], Active, ProductImage, qty,treatmentType, BRAND.BrandID, BRAND.Name, BRAND.[Type(T/A)]


 FROM PRODUCT, TREATMENT, BRAND
 Where ProductID = TreatmentID AND BRAND.BrandID = TREATMENT.BrandID
 ORDER BY [ProductType(T/A/S)]

END
