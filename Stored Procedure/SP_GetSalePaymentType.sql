-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	given sale id returns the payment type
-- =============================================
CREATE PROCEDURE SP_GetSalePaymentType 
	@SaleID nchar(10)
AS
BEGIN
	Select PaymentType
	From SALE
	where SaleID = @SaleID
END
GO
