USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a SaleID, ProductID & Qty is Creates a salesDTL record
-- =============================================
Create PROCEDURE [dbo].[SP_CreateSalesDTLRecord]
	@SaleID nchar(10), 
	@ProductID nchar(10),
	@Qty int
AS
BEGIN
	begin try
		begin transaction
			INSERt INTO SALES_DTL(SaleID, ProductID, Qty, Price) 
				values(@SaleID, @ProductID, @Qty, (select Price
													from PRODUCT
													where ProductID = @ProductID))
								commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
