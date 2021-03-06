USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AccessorySearchByTerm]    Script Date: 2018/07/04 12:06:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter  PROCEDURE [dbo].[SP_AccessorySearchByTerm] 
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P, ACCESSORY A, BRAND B
	WHERE ProductID = AccessoryID AND B.BrandID = A.BrandID AND 
	(P.Name like '%'+@searchTerm+'%'  
	Or P.ProductDescription like '%'+@searchTerm+'%'
	Or A.Colour like '%'+@searchTerm+'%'
	Or B.Name like '%'+@searchTerm+'%') 
	AND P.Active = 'Y'
	Order By P.[ProductType(T/A/S)]
END
