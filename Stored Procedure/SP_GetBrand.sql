SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns Brand details given ID
-- =============================================
alter PROCEDURE SP_GetBrand
	@BrandID nchar(10)
AS
BEGIN
	Select *
	From BRAND
	Where BrandID = @BrandID
END
GO
