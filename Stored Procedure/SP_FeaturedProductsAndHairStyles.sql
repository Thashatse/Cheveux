SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets the Featured Products and hairstyles
-- =============================================
alter PROCEDURE SP_FeaturedProductsAndHairStyles 
AS
BEGIN
	SELECT [FeatureID], [ItemID], [ImageURL], PRODUCT.Name, PRODUCT.ProductDescription, PRODUCT.Price
	FROM [CHEVEUX].[dbo].[Home_Page], PRODUCT
	Where ItemID = PRODUCT.ProductID
	Order By [FeatureID]
END
GO
