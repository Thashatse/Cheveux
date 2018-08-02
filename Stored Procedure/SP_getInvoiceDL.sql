SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE SP_getInvoiceDL 
	@BookingID nchar(10)
AS
BEGIN
	Select p.[Name], d.Qty, d.Price, p.ProductID, p.[ProductType(T/A/S)]
	From SALE s, SALES_DTL d, PRODUCT p
	Where s.SaleID = d.SaleID
	AND s.BookingID = @BookingID
	AND p.ProductID = d.ProductID
END
GO
