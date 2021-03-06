USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_BraidServiceSearchByTerm]    Script Date: 2018/07/04 12:40:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter  PROCEDURE [dbo].[SP_BraidServiceSearchByTerm]
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P, SERVICE S, BRAID_SERVICE B, STYLE Y, LENGTH L, WIDTH W
	Where P.ProductID = s.ServiceID AND s.ServiceID = B.ServiceID AND B.LengthID = L.LengthID
	AND B.ServiceID = Y.StyleID AND B.WidthID = W.WidthID
	AND (P.Name like '%'+@searchTerm+'%'  
	Or P.ProductDescription like '%'+@searchTerm+'%' 
	OR W.Description like '%'+@searchTerm+'%'
	OR Y.Description like '%'+@searchTerm+'%'
	OR L.Description like '%'+@searchTerm+'%') AND P.Active = 'Y'
	AND P.[ProductType(T/A/S)] = 'S'
	Order By P.[ProductType(T/A/S)]
END
