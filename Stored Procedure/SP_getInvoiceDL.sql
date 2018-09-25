SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE SP_getInvoiceDL 
	@BookingID nchar(10)
AS
BEGIN
	Select p.[Name], d.Qty, d.Price, p.ProductID, p.[ProductType(T/A/S)]
	From SALES_DTL d, PRODUCT p
	Where d.SaleID = @BookingID
	AND p.ProductID = d.ProductID
END
GO
