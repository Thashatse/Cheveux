SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the features stylists and contact us info
-- =============================================
alter  PROCEDURE SP_FeaturedProductsAndContact 
AS
BEGIN
	SELECT [FeatureID], [ItemID], [ImageURL], [USER].FirstName, [USER].ContactNo, [USER].Email
	FROM [CHEVEUX].[dbo].[Home_Page], [USER]
	Where ItemID = [USER].UserID
END
GO
