-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all outstanding product stock orders
-- =============================================
alter PROCEDURE SP_GetOutstandingProductOrders
AS
BEGIN
	Select * 
	From [Order], Supplier
	Where Received = 'false'
		AND [Order].SupplierID = Supplier.SupplierID
END
GO
