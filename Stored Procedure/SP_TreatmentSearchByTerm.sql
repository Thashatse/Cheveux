USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_TreatmentSearchByTerm]    Script Date: 2018/07/04 12:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_TreatmentSearchByTerm] 
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P, TREATMENT T, BRAND B
	WHERE ProductID = TreatmentID AND B.BrandID = T.BrandID AND 
	(P.Name like '%'+@searchTerm+'%'  
	Or P.ProductDescription like '%'+@searchTerm+'%'
	Or T.TreatmentType like '%'+@searchTerm+'%'
	Or B.Name like '%'+@searchTerm+'%')  
	AND P.Active = 'Y'
	Order By P.[ProductType(T/A/S)]
END
