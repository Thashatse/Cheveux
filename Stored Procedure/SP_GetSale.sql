SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	get Sale Details
-- =============================================
alter PROCEDURE SP_GetSale
	@saleID nchar(10)
AS
BEGIN
	SELECT *
	From SALE
	Where SaleID = @saleID
END
GO
