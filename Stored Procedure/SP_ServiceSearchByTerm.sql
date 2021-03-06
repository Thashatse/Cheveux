USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_ServiceSearchByTerm]    Script Date: 2018/07/04 12:43:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_ServiceSearchByTerm]
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P
	Where P.ProductID not in
			(SELECT [ServiceID] FROM [CHEVEUX].[dbo].[BRAID_SERVICE])
	AND (P.Name like '%'+@searchTerm+'%'  Or P.ProductDescription like '%'+@searchTerm+'%') AND P.Active = 'Y'
	AND P.[ProductType(T/A/S)] = 'S'
	Order By P.[ProductType(T/A/S)]
END
