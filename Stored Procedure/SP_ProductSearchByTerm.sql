USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductSearchByTerm]    Script Date: 2018/05/19 19:14:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_ProductSearchByTerm]
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P
	Where (P.Name like '%'+@searchTerm+'%'  Or P.ProductDescription like '%'+@searchTerm+'%') AND P.Active = 'Y'
	Order By P.[ProductType(T/A/S)]
END
