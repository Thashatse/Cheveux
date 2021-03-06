USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBrandsForProductType]    Script Date: 2018/08/28 12:38:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		L.Human
-- =============================================
alter PROCEDURE SP_GetBrandsForProductType
	@productType char(1)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BrandID, [Name], [Type(T/A)]
    FROM BRAND
	WHERE BRAND.[Type(T/A)]=@productType
END
