USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_Products]    Script Date: 8/11/2018 4:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter PROCEDURE [dbo].[SP_Products]

@productID nchar(10)



AS
BEGIN
	
SELECT ProductID, Product.Name, ProductDescription, Price, [ProductType(T/A/S)], Active, ProductImage, colour, qty,BRAND.BrandID, BRAND.Name, BRAND.[Type(T/A)]


 FROM PRODUCT, ACCESSORY, BRAND
 Where ProductID = AccessoryID AND BRAND.BrandID = ACCESSORY.BrandID
 ORDER BY [ProductType(T/A/S)]

END
