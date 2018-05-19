SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_SearchByTermAndType
	@SearchTerm nchar(50),
	@ProdType char(1)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.ProductID
	From PRODUCT P
	Where P.[ProductType(T/A/S)] = @ProdType AND P.Name like '%'+@SearchTerm+'%' Or P.ProductDescription like '%'+@SearchTerm+'%'
END
GO
