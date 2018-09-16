-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all past product stock orders
-- =============================================
Alter PROCEDURE SP_GetPastProductOrders
AS
BEGIN
	Select * 
	From [Order], Supplier
	Where Received = 'true'
		AND [Order].SupplierID = Supplier.SupplierID
END
GO
