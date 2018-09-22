SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	check for a matching exisiting Brand with given ID
-- =============================================
create  PROCEDURE SP_CheckForBrand 
	@BrandID nchar(10)
AS
BEGIN
	SELECT BrandID 
	FROM [BRAND]
	WHERE BrandID = @BrandID
END
GO